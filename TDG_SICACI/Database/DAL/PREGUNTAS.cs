using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using TDG_SICACI.Database.DAL;

namespace TDG_SICACI.Database.DAL
{
    public interface IPreguntas
    {
        IEnumerable<SP_GET_LISTPREGUNTA_MODEL> GetPreguntaList();
        IEnumerable<SP_GET_NORMA_ISO_MODEL> GetNormaISO();
        void NewPregunta_Abierta(string texto, string comentario, string documento, int pregunta_norma, int norma_iso, int orden_visual, string usuario);
        void NewPregunta_Multiple(string texto, string comentario, string documento, int pregunta_norma, int norma_iso, int orden_visual, string usuario, 
            string tipo_pregunta, IEnumerable<Models.RespuestasViewModel> respuestas);
        void EliminarPreguntaGIDEM(int id);
        IEnumerable<SP_CONSTRUIR_SELF_MODEL> GetInfoSelf();
    }


    public partial class SICACI_DAL : IPreguntas
    {
        public IPreguntas IPreguntas
        {
            get
            {
                return (IPreguntas)this;
            }
        }

        IEnumerable<SP_GET_LISTPREGUNTA_MODEL> IPreguntas.GetPreguntaList()
        {
            try
            {
                using (SICACIEntities cnn = new SICACIEntities())
                {
                    return cnn.SP_GET_PREGUNTAS().ToArray();
                }
            }
            catch (Exception ex)
            {
                if (ex.InnerException is SqlException) throw ex.InnerException;
                throw new Exception(string.Format("{0} {1}", JertiFramework.My.Resources.JFLibraryErrors.Error_Try_Catch_Server, ex.Message), ex);
            }
        }

        IEnumerable<SP_GET_NORMA_ISO_MODEL> IPreguntas.GetNormaISO()
        {
            try
            {
                using (SICACIEntities cnn = new SICACIEntities())
                {
                    return cnn.SP_GET_NORMA_ISO().ToArray();
                }
            }
            catch (Exception ex)
            {
                if (ex.InnerException is SqlException) throw ex.InnerException;
                throw new Exception(string.Format("{0} {1}", JertiFramework.My.Resources.JFLibraryErrors.Error_Try_Catch_Server, ex.Message), ex);
            }
        }


        void IPreguntas.NewPregunta_Abierta(string texto, string comentario, string documento, int pregunta_norma, int norma_iso, int orden_visual, string usuario)
        {
            try
            {
                using (SICACIEntities cnn = new SICACIEntities())
                {
                    cnn.SP_NEW_PREGUNTA_ABIERTA(pregunta_norma, usuario, texto, comentario, documento, norma_iso, orden_visual); 
                }
            }
            catch (Exception ex)
            {
                if (ex.InnerException is SqlException) throw ex.InnerException;
                throw new Exception(string.Format("{0} {1}", JertiFramework.My.Resources.JFLibraryErrors.Error_Try_Catch_Server, ex.Message), ex);
            }
        }


        void IPreguntas.NewPregunta_Multiple(string texto, string comentario, string documento, int pregunta_norma, int norma_iso, int orden_visual, string usuario, string tipo_pregunta, IEnumerable<Models.RespuestasViewModel> respuestas)
        {
            /*STEP 1: Definimos la TVP definida en SQL Server*/
            DataTable udt_respuestas = new DataTable();
            udt_respuestas.Columns.Add("TPM_DESCRIPCION", typeof(string));
            udt_respuestas.Columns.Add("ORDEN", typeof(int));
            udt_respuestas.Columns.Add("COMENTARIO", typeof(string));
            udt_respuestas.Columns.Add("ES_CORRECTA", typeof(string));

            /*STEP 2: Convertimos la enumeración "Respuestas" al DataTable*/
            foreach (Models.RespuestasViewModel item in respuestas)
            {
                udt_respuestas.Rows.Add(item.Descripcion, item.Orden, item.Comentario, item.EsCorrecta);
            }

            /*STEP 3: Definimos todos los parametros que lleva el SP en la base de datos*/
            SqlParameter parm_TipoPregunta = new SqlParameter("tipo_preg", SqlDbType.Char, 2);
            SqlParameter parm_PreguntaNorma = new SqlParameter("es_preg_norma", SqlDbType.Int);
            SqlParameter parm_usuario = new SqlParameter("usuario", SqlDbType.VarChar, 12);
            SqlParameter parm_pregunta = new SqlParameter("pregunta", SqlDbType.VarChar, 200);
            SqlParameter parm_comentario = new SqlParameter("comentario", SqlDbType.VarChar, 100);
            SqlParameter parm_Tipodoc = new SqlParameter("tipo_doc", SqlDbType.Char, 3);
            SqlParameter parm_Categoria = new SqlParameter("cat_pertenece", SqlDbType.Int);
            SqlParameter parm_OrdenVisual = new SqlParameter("orden_visual_padd", SqlDbType.Int);
            SqlParameter parm_Respuestas = new SqlParameter("multiple", SqlDbType.Structured);

            parm_TipoPregunta.Value = tipo_pregunta;
            parm_PreguntaNorma.Value = pregunta_norma;
            parm_usuario.Value = usuario;
            parm_pregunta.Value = texto;
            if (string.IsNullOrWhiteSpace(comentario))
                parm_comentario.Value = DBNull.Value;
            else
                parm_comentario.Value = comentario;
            parm_Tipodoc.Value = documento;
            parm_Categoria.Value = norma_iso;
            parm_OrdenVisual.Value = orden_visual;
            parm_Respuestas.TypeName = "dbo.Preg_Multiple";
            parm_Respuestas.Value = udt_respuestas;

            /*STEP 4: Enviamos el comando al servidor de BD*/
            SICACIEntities cnn = new SICACIEntities();
            try
            {
                cnn.Database.ExecuteSqlCommand("SP_Crear_Pregunta_Multiple @tipo_preg, @es_preg_norma, @usuario, @pregunta, @comentario, @tipo_doc, @cat_pertenece, @orden_visual_padd, @multiple",
                    parm_TipoPregunta, parm_PreguntaNorma, parm_usuario, parm_pregunta, parm_comentario,
                    parm_Tipodoc, parm_Categoria, parm_OrdenVisual, parm_Respuestas);
            }
            catch (Exception ex)
            {
                if (ex.InnerException is SqlException) throw ex.InnerException;
                throw new Exception(string.Format("{0} {1}", "Ocurrio un error al intentar crear la pregunta.", ex.Message), ex);
            }
        }


        void IPreguntas.EliminarPreguntaGIDEM(int id)
        {
            try
            {
                using (SICACIEntities cnn = new SICACIEntities())
                {
                    cnn.SP_DELETE_PREGUNTA_GIDEM(id);
                }
            }
            catch (Exception ex)
            {
                if (ex.InnerException is SqlException) throw ex.InnerException;
                throw new Exception(string.Format("{0} {1}", JertiFramework.My.Resources.JFLibraryErrors.Error_Try_Catch_Server, ex.Message), ex);
            }
        }


        IEnumerable<SP_CONSTRUIR_SELF_MODEL> IPreguntas.GetInfoSelf()
        {
            try
            {
                using (SICACIEntities cnn = new SICACIEntities())
                {
                    return cnn.SP_CONSTRUIR_SELF().ToArray();
                }
            }
            catch (Exception ex)
            {
                if (ex.InnerException is SqlException) throw ex.InnerException;
                throw new Exception(string.Format("{0} {1}", JertiFramework.My.Resources.JFLibraryErrors.Error_Try_Catch_Server, ex.Message), ex);
            }
        }
    }
}