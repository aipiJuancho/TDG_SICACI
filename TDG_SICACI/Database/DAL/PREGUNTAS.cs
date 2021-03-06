﻿using System;
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
        void NewPregunta_Abierta(string texto, string comentario, string documento, int pregunta_norma, int norma_iso, int orden_visual, string usuario, string link_comentario);
        void NewPregunta_Multiple(string texto, string comentario, string documento, int pregunta_norma, int norma_iso, int orden_visual, string usuario,
            string tipo_pregunta, IEnumerable<Models.RespuestasViewModel> respuestas, string link_comentario);
        void EliminarPreguntaGIDEM(int id);
        IEnumerable<SP_CONSTRUIR_SELF_MODEL> GetInfoSelf();
        void ModificarPreguntaGIDEM(int id_pregunta, int orden_visual);
        SP_GET_PREGUNTA_MODEL GetPregunta(int id_pregunta);
        int SaveEvaluacion(string usuario, DataTable solucion);
        void AsociarDocumento_Respuesta(int ID_Solucion, string ID_pregunta, string archivo);
        IEnumerable<SP_GET_EVALUACIONES_MODEL> GetEvaluaciones();
        IEnumerable<SP_CONSULTAR_SOLUCION_LAST_COMENTARIO_MODEL> GetComentarios();
        void SaveComentario(string comentario, int id, string user);
        SP_CONSULTAR_REVISION_INFOGENERAL_MODEL Revision_Info(int id);
        IEnumerable<SP_CONSULTAR_REVISION_INFOJERARQUIA_MODEL> Revision_GruposJerarquia(int id);
        IEnumerable<SP_CONSULTAR_REVISION_PREGUNTASYRESPUESTAS_MODEL> Revision_PreguntasYRespuestas(int solucion, int grupo);
        void GuardarRevision(int solucion, string preguntas, string respuestas, string usuario);
        IEnumerable<SP_CONSULTAR_CALIFICACION_REVISIONES_MODEL> GetCalificaciones();
        void MarcarRevisionEvaluacion(int evaluacion, string usuario);
        IEnumerable<SP_CONSULTAR_EVALUACIONES_ACTIVAS_MODEL> GetEvaluacionesPendientes();
        int SaveTempEvaluacion(string usuario, DataTable solucion);
        IEnumerable<SP_CONSTRUIR_SELF_MODEL> GetInfoSelfTemporal(int id);
        IEnumerable<SP_GET_NORMA_ISO_MODEL> GetNormaISOTemporal(int id);
        IEnumerable<SP_GET_RESPONSES_EVALUACIONTEMP_MODEL> GetResponsesTemp(int id);
        void EliminarEvaluacionTemporal(int id);
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


        void IPreguntas.NewPregunta_Abierta(string texto, string comentario, string documento, int pregunta_norma, int norma_iso, int orden_visual, string usuario, string link_comentario)
        {
            try
            {
                using (SICACIEntities cnn = new SICACIEntities())
                {
                    cnn.SP_NEW_PREGUNTA_ABIERTA(pregunta_norma, usuario, texto, comentario, documento, norma_iso, orden_visual, link_comentario); 
                }
            }
            catch (Exception ex)
            {
                if (ex.InnerException is SqlException) throw ex.InnerException;
                throw new Exception(string.Format("{0} {1}", JertiFramework.My.Resources.JFLibraryErrors.Error_Try_Catch_Server, ex.Message), ex);
            }
        }


        void IPreguntas.NewPregunta_Multiple(string texto, string comentario, string documento, int pregunta_norma, int norma_iso, int orden_visual, string usuario, string tipo_pregunta, IEnumerable<Models.RespuestasViewModel> respuestas, string link_comentario)
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
            SqlParameter parm_comentario = new SqlParameter("comentario", SqlDbType.VarChar, 500);
            SqlParameter parm_Tipodoc = new SqlParameter("tipo_doc", SqlDbType.Char, 3);
            SqlParameter parm_Categoria = new SqlParameter("cat_pertenece", SqlDbType.Int);
            SqlParameter parm_OrdenVisual = new SqlParameter("orden_visual_padd", SqlDbType.Int);
            SqlParameter parm_Respuestas = new SqlParameter("multiple", SqlDbType.Structured);
            SqlParameter parm_link_comentario = new SqlParameter("LINK_COMENTARIO", SqlDbType.VarChar, 500);

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
            if (string.IsNullOrWhiteSpace(link_comentario))
                parm_link_comentario.Value = DBNull.Value;
            else
                parm_link_comentario.Value = link_comentario;

            /*STEP 4: Enviamos el comando al servidor de BD*/
            SICACIEntities cnn = new SICACIEntities();
            try
            {
                cnn.Database.ExecuteSqlCommand("SP_Crear_Pregunta_Multiple @tipo_preg, @es_preg_norma, @usuario, @pregunta, @comentario, @tipo_doc, @cat_pertenece, @orden_visual_padd, @multiple, @LINK_COMENTARIO",
                    parm_TipoPregunta, parm_PreguntaNorma, parm_usuario, parm_pregunta, parm_comentario,
                    parm_Tipodoc, parm_Categoria, parm_OrdenVisual, parm_Respuestas, parm_link_comentario);
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


        void IPreguntas.ModificarPreguntaGIDEM(int id_pregunta, int orden_visual)
        {
            try
            {
                using (SICACIEntities cnn = new SICACIEntities())
                {
                    cnn.SP_UPDATE_PREGUNTA_ADD(id_pregunta, orden_visual);
                }
            }
            catch (Exception ex)
            {
                if (ex.InnerException is SqlException) throw ex.InnerException;
                throw new Exception(string.Format("{0} {1}", JertiFramework.My.Resources.JFLibraryErrors.Error_Try_Catch_Server, ex.Message), ex);
            }
        }


        SP_GET_PREGUNTA_MODEL IPreguntas.GetPregunta(int id_pregunta)
        {
            try
            {
                using (SICACIEntities cnn = new SICACIEntities())
                {
                    return cnn.SP_GET_PREGUNTA(id_pregunta).FirstOrDefault();
                }
            }
            catch (Exception ex)
            {
                if (ex.InnerException is SqlException) throw ex.InnerException;
                throw new Exception(string.Format("{0} {1}", JertiFramework.My.Resources.JFLibraryErrors.Error_Try_Catch_Server, ex.Message), ex);
            }
        }


        int IPreguntas.SaveEvaluacion(string usuario, DataTable solucion)
        {
            /*STEP 1: Definimos todos los parametros que lleva el SP en la base de datos*/
            SqlParameter parm_Usuario = new SqlParameter("USUARIO", SqlDbType.VarChar, 12);
            SqlParameter parm_Solucion = new SqlParameter("SOLUCION", SqlDbType.Structured);
            SqlParameter parm_ID_SelfA = new SqlParameter("ID_SELFA", SqlDbType.Int);
            parm_ID_SelfA.Direction = ParameterDirection.Output;

            parm_Usuario.Value = usuario;
            parm_Solucion.TypeName = "dbo.Solucion";
            parm_Solucion.Value = solucion;

            /*STEP 4: Enviamos el comando al servidor de BD*/
            SICACIEntities cnn = new SICACIEntities();
            try
            {
                cnn.Database.ExecuteSqlCommand("SP_Crear_Solucion @USUARIO, @SOLUCION, @ID_SELFA OUTPUT",
                    parm_Usuario, parm_Solucion, parm_ID_SelfA);
                return (int)parm_ID_SelfA.Value;
            }
            catch (Exception ex)
            {
                if (ex.InnerException is SqlException) throw ex.InnerException;
                throw new Exception(string.Format("{0}. {1}", "Ocurrio un error al intentar guardar la solución del Self-Assessment", ex.Message), ex);
            }
            
        }


        void IPreguntas.AsociarDocumento_Respuesta(int ID_Solucion, string ID_pregunta, string archivo)
        {
            try
            {
                using (SICACIEntities cnn = new SICACIEntities())
                {
                    cnn.SP_ASOCIAR_DOCUMENTO_RESPUESTA(ID_Solucion, int.Parse(ID_pregunta), archivo);
                }
            }
            catch (Exception ex)
            {
                if (ex.InnerException is SqlException) throw ex.InnerException;
                throw new Exception(string.Format("{0} {1}", JertiFramework.My.Resources.JFLibraryErrors.Error_Try_Catch_Server, ex.Message), ex);
            }
        }


        IEnumerable<SP_GET_EVALUACIONES_MODEL> IPreguntas.GetEvaluaciones()
        {
            try
            {
                using (SICACIEntities cnn = new SICACIEntities())
                {
                    return cnn.SP_GET_EVALUACIONES().ToArray();
                }
            }
            catch (Exception ex)
            {
                if (ex.InnerException is SqlException) throw ex.InnerException;
                throw new Exception(string.Format("{0} {1}", JertiFramework.My.Resources.JFLibraryErrors.Error_Try_Catch_Server, ex.Message), ex);
            }
        }


        IEnumerable<SP_CONSULTAR_SOLUCION_LAST_COMENTARIO_MODEL> IPreguntas.GetComentarios()
        {
            try
            {
                using (SICACIEntities cnn = new SICACIEntities())
                {
                    return cnn.SP_CONSULTAR_SOLUCION_LAST_COMENTARIO().ToArray();
                }
            }
            catch (Exception ex)
            {
                if (ex.InnerException is SqlException) throw ex.InnerException;
                throw new Exception(string.Format("{0} {1}", JertiFramework.My.Resources.JFLibraryErrors.Error_Try_Catch_Server, ex.Message), ex);
            }
        }


        void IPreguntas.SaveComentario(string comentario, int id, string user)
        {
            try
            {
                using (SICACIEntities cnn = new SICACIEntities())
                {
                    cnn.SP_CREAR_SOLUCION_COMENTARIO(id, comentario, user);
                }
            }
            catch (Exception ex)
            {
                if (ex.InnerException is SqlException) throw ex.InnerException;
                throw new Exception(string.Format("{0} {1}", JertiFramework.My.Resources.JFLibraryErrors.Error_Try_Catch_Server, ex.Message), ex);
            }
        }


        SP_CONSULTAR_REVISION_INFOGENERAL_MODEL IPreguntas.Revision_Info(int id)
        {
            try
            {
                using (SICACIEntities cnn = new SICACIEntities())
                {
                    return cnn.SP_CONSULTAR_REVISION_INFOGENERAL().Where(r => r.ID_SOLUCION.Equals(id)).First();
                }
            }
            catch (Exception ex)
            {
                if (ex.InnerException is SqlException) throw ex.InnerException;
                throw new Exception(string.Format("{0} {1}", JertiFramework.My.Resources.JFLibraryErrors.Error_Try_Catch_Server, ex.Message), ex);
            }
        }


        IEnumerable<SP_CONSULTAR_REVISION_INFOJERARQUIA_MODEL> IPreguntas.Revision_GruposJerarquia(int id)
        {
            try
            {
                using (SICACIEntities cnn = new SICACIEntities())
                {
                    return cnn.SP_CONSULTAR_REVISION_INFOJERARQUIA(id).ToArray();
                }
            }
            catch (Exception ex)
            {
                if (ex.InnerException is SqlException) throw ex.InnerException;
                throw new Exception(string.Format("{0} {1}", JertiFramework.My.Resources.JFLibraryErrors.Error_Try_Catch_Server, ex.Message), ex);
            }
        }


        IEnumerable<SP_CONSULTAR_REVISION_PREGUNTASYRESPUESTAS_MODEL> IPreguntas.Revision_PreguntasYRespuestas(int solucion, int grupo)
        {
            try
            {
                using (SICACIEntities cnn = new SICACIEntities())
                {
                    return cnn.SP_CONSULTAR_REVISION_PREGUNTASRESPUESTAS_BYGRUPO(solucion, grupo).ToArray();
                }
            }
            catch (Exception ex)
            {
                if (ex.InnerException is SqlException) throw ex.InnerException;
                throw new Exception(string.Format("{0} {1}", JertiFramework.My.Resources.JFLibraryErrors.Error_Try_Catch_Server, ex.Message), ex);
            }
        }


        void IPreguntas.GuardarRevision(int solucion, string preguntas, string respuestas, string usuario)
        {
            try
            {
                using (SICACIEntities cnn = new SICACIEntities())
                {
                    cnn.SP_SAVE_REVISION(solucion, preguntas, respuestas, usuario);
                }
            }
            catch (Exception ex)
            {
                if (ex.InnerException is SqlException) throw ex.InnerException;
                throw new Exception(string.Format("{0} {1}", JertiFramework.My.Resources.JFLibraryErrors.Error_Try_Catch_Server, ex.Message), ex);
            }
        }


        IEnumerable<SP_CONSULTAR_CALIFICACION_REVISIONES_MODEL> IPreguntas.GetCalificaciones()
        {
            try
            {
                using (SICACIEntities cnn = new SICACIEntities())
                {
                    return cnn.SP_CONSULTAR_CALIFICACION_REVISIONES().ToArray();
                }
            }
            catch (Exception ex)
            {
                if (ex.InnerException is SqlException) throw ex.InnerException;
                throw new Exception(string.Format("{0} {1}", JertiFramework.My.Resources.JFLibraryErrors.Error_Try_Catch_Server, ex.Message), ex);
            }
        }


        void IPreguntas.MarcarRevisionEvaluacion(int evaluacion, string usuario)
        {
            try
            {
                using (SICACIEntities cnn = new SICACIEntities())
                {
                    cnn.SP_MARCAR_REVISADO_EVALUACION(evaluacion, usuario);
                }
            }
            catch (Exception ex)
            {
                if (ex.InnerException is SqlException) throw ex.InnerException;
                throw new Exception(string.Format("{0} {1}", JertiFramework.My.Resources.JFLibraryErrors.Error_Try_Catch_Server, ex.Message), ex);
            }
        }


        IEnumerable<SP_CONSULTAR_EVALUACIONES_ACTIVAS_MODEL> IPreguntas.GetEvaluacionesPendientes()
        {
            try
            {
                using (SICACIEntities cnn = new SICACIEntities())
                {
                    return cnn.SP_CONSULTAR_EVALUACIONES_ACTIVAS().ToArray();
                }
            }
            catch (Exception ex)
            {
                if (ex.InnerException is SqlException) throw ex.InnerException;
                throw new Exception(string.Format("{0} {1}", JertiFramework.My.Resources.JFLibraryErrors.Error_Try_Catch_Server, ex.Message), ex);
            }
        }


        int IPreguntas.SaveTempEvaluacion(string usuario, DataTable solucion)
        {
            /*STEP 1: Definimos todos los parametros que lleva el SP en la base de datos*/
            SqlParameter parm_Usuario = new SqlParameter("USUARIO", SqlDbType.VarChar, 12);
            SqlParameter parm_Solucion = new SqlParameter("SOLUCION", SqlDbType.Structured);
            SqlParameter parm_ID_SelfA = new SqlParameter("ID_SELFA", SqlDbType.Int);
            parm_ID_SelfA.Direction = ParameterDirection.Output;

            parm_Usuario.Value = usuario;
            parm_Solucion.TypeName = "dbo.Solucion";
            parm_Solucion.Value = solucion;

            /*STEP 4: Enviamos el comando al servidor de BD*/
            SICACIEntities cnn = new SICACIEntities();
            try
            {
                cnn.Database.ExecuteSqlCommand("SP_CREAR_SOLUCION_TEMPORAL @USUARIO, @SOLUCION, @ID_SELFA OUTPUT",
                    parm_Usuario, parm_Solucion, parm_ID_SelfA);
                return (int)parm_ID_SelfA.Value;
            }
            catch (Exception ex)
            {
                if (ex.InnerException is SqlException) throw ex.InnerException;
                throw new Exception(string.Format("{0}. {1}", "Ocurrio un error al intentar guardar temporalmente la evaluación", ex.Message), ex);
            }
        }


        IEnumerable<SP_CONSTRUIR_SELF_MODEL> IPreguntas.GetInfoSelfTemporal(int id)
        {
            try
            {
                using (SICACIEntities cnn = new SICACIEntities())
                {
                    return cnn.SP_LISTAR_EVALUACIONTEMP(id).ToArray();
                }
            }
            catch (Exception ex)
            {
                if (ex.InnerException is SqlException) throw ex.InnerException;
                throw new Exception(string.Format("{0} {1}", JertiFramework.My.Resources.JFLibraryErrors.Error_Try_Catch_Server, ex.Message), ex);
            }
        }


        IEnumerable<SP_GET_NORMA_ISO_MODEL> IPreguntas.GetNormaISOTemporal(int id)
        {
            try
            {
                using (SICACIEntities cnn = new SICACIEntities())
                {
                    return cnn.SP_GET_NORMAISO_TEMPORAL(id).ToArray();
                }
            }
            catch (Exception ex)
            {
                if (ex.InnerException is SqlException) throw ex.InnerException;
                throw new Exception(string.Format("{0} {1}", JertiFramework.My.Resources.JFLibraryErrors.Error_Try_Catch_Server, ex.Message), ex);
            }
        }


        IEnumerable<SP_GET_RESPONSES_EVALUACIONTEMP_MODEL> IPreguntas.GetResponsesTemp(int id)
        {
            try
            {
                using (SICACIEntities cnn = new SICACIEntities())
                {
                    return cnn.SP_GET_RESPONSES_EVALUACIONTEMP(id).ToArray();
                }
            }
            catch (Exception ex)
            {
                if (ex.InnerException is SqlException) throw ex.InnerException;
                throw new Exception(string.Format("{0} {1}", JertiFramework.My.Resources.JFLibraryErrors.Error_Try_Catch_Server, ex.Message), ex);
            }
        }


        void IPreguntas.EliminarEvaluacionTemporal(int id)
        {
            try
            {
                using (SICACIEntities cnn = new SICACIEntities())
                {
                    cnn.SP_DELETE_EVALUACION_TEMPORAL(id);
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