using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace TDG_SICACI.Database.DAL
{
    public interface IProyectos
    {
        void NuevoProyecto(string Nombre, string Resp_Ejecucion, string Resp_Aprobacion, string Objetivos, string Findings, DateTime FInicio, string User);
        IEnumerable<SP_GRID_PROYECTOS_MODEL> GetGridData();
        IEnumerable<SP_CONSULTAR_PROYECTOS_MODEL> Consultar();
        IEnumerable<SP_CONSULTAR_OBJETIVOS_ASOCIADO_PROYECTO_MODEL> ConsultarObjetivosProyecto();
        void ModificarProyecto(int ID, string Nombre, string Resp_Ejecucion, string Resp_Aprobacion, string Objetivos, string Findings, DateTime FInicio, string User, string Estado);
        void EliminarProyecto(int ID);
        IEnumerable<SP_GRID_TAREAS_MODEL> GetTareas();
        void CrearTarea(int IDProyecto, int Orden, string Titulo, string Descripcion, string Responsable, string Recursos, DateTime FechaFin, decimal Progreso, string PersonalInvolucrado, string UserCreador);
        SP_CONSULTAR_TAREA_INFO_MODEL ConsultarInfo_Tarea(int id);
        IEnumerable<SP_CONSULTAR_TAREA_INFO_MODEL> ConsultarTareas();
        IEnumerable<SP_CONSULTAR_TAREA_PERSONAL_MODEL> ConsultarPersonal_Tarea(int id);
        IEnumerable<SP_CONSULTAR_TAREA_ARCHIVOS_MODEL> ConsultarArchivos_Tarea(int id);
        string VincularArchivo_Tarea(int id, string titulo, string extensionFile, string usuario);
        IEnumerable<SP_CONSULTAR_TAREA_COMENTARIOS_BYID_MODEL> ConsultarComentarios_Tarea(int id);
        void CrearComentario_Tarea(int id, string comentario, string user);
        void ModificarTarea(int IDTarea, int Orden, string Titulo, string Descripcion, string Responsable, string Recursos, DateTime FechaFin, decimal Progreso, string PersonalInvolucrado, string UserCreador);
        void EliminarTarea(int IDTarea, string User);
        void EliminarArchivo_Tarea(int IDTarea, string fileName, string User);
        IEnumerable<SP_GRID_PROYECTOS_MODEL> GetGridData_ByUser(string user);
        IEnumerable<SP_GRID_TAREAS_MODEL> GetTareas_ByUser(string user, int ID);
    }


    public partial class SICACI_DAL : IProyectos
    {
        public IProyectos IProyectos
        {
            get
            {
                return (IProyectos)this;
            }
        }
    
        void IProyectos.NuevoProyecto(string Nombre, string Resp_Ejecucion, string Resp_Aprobacion, string Objetivos, string Findings, DateTime FInicio, string User)
        {
 	        try
            {
                using (SICACIEntities cnn = new SICACIEntities())
                {
                    cnn.SP_CREAR_PROYECTO(Nombre, Resp_Ejecucion, Resp_Aprobacion, Objetivos, Findings, FInicio, User);
                }
            }
            catch (Exception ex)
            {
                if (ex.InnerException is SqlException) throw ex.InnerException;
                throw new Exception(string.Format("{0} {1}", JertiFramework.My.Resources.JFLibraryErrors.Error_Try_Catch_Server, ex.Message), ex);
            }
        }


        IEnumerable<SP_GRID_PROYECTOS_MODEL> IProyectos.GetGridData()
        {
            try
            {
                using (SICACIEntities cnn = new SICACIEntities())
                {
                    return cnn.SP_GRID_PROYECTOS().ToArray();
                }
            }
            catch (Exception ex)
            {
                if (ex.InnerException is SqlException) throw ex.InnerException;
                throw new Exception(string.Format("{0} {1}", JertiFramework.My.Resources.JFLibraryErrors.Error_Try_Catch_Server, ex.Message), ex);
            }
        }


        IEnumerable<SP_CONSULTAR_PROYECTOS_MODEL> IProyectos.Consultar()
        {
            try
            {
                using (SICACIEntities cnn = new SICACIEntities())
                {
                    return cnn.SP_CONSULTAR_PROYECTOS().ToArray();
                }
            }
            catch (Exception ex)
            {
                if (ex.InnerException is SqlException) throw ex.InnerException;
                throw new Exception(string.Format("{0} {1}", JertiFramework.My.Resources.JFLibraryErrors.Error_Try_Catch_Server, ex.Message), ex);
            }
        }


        IEnumerable<SP_CONSULTAR_OBJETIVOS_ASOCIADO_PROYECTO_MODEL> IProyectos.ConsultarObjetivosProyecto()
        {
            try
            {
                using (SICACIEntities cnn = new SICACIEntities())
                {
                    return cnn.SP_CONSULTAR_OBJETIVOS_ASOCIADO_PROYECTO().ToArray();
                }
            }
            catch (Exception ex)
            {
                if (ex.InnerException is SqlException) throw ex.InnerException;
                throw new Exception(string.Format("{0} {1}", JertiFramework.My.Resources.JFLibraryErrors.Error_Try_Catch_Server, ex.Message), ex);
            }
        }


        void IProyectos.ModificarProyecto(int ID, string Nombre, string Resp_Ejecucion, string Resp_Aprobacion, string Objetivos, string Findings, DateTime FInicio, string User, string Estado)
        {
            try
            {
                using (SICACIEntities cnn = new SICACIEntities())
                {
                    cnn.SP_MODIFICAR_PROYECTO(ID, Nombre, Resp_Ejecucion, Resp_Aprobacion, Objetivos, Findings, FInicio, Estado, User);
                }
            }
            catch (Exception ex)
            {
                if (ex.InnerException is SqlException) throw ex.InnerException;
                throw new Exception(string.Format("{0} {1}", JertiFramework.My.Resources.JFLibraryErrors.Error_Try_Catch_Server, ex.Message), ex);
            }
        }


        void IProyectos.EliminarProyecto(int ID)
        {
            try
            {
                using (SICACIEntities cnn = new SICACIEntities())
                {
                    cnn.SP_DELETE_PROYECTO(ID);
                }
            }
            catch (Exception ex)
            {
                if (ex.InnerException is SqlException) throw ex.InnerException;
                throw new Exception(string.Format("{0} {1}", JertiFramework.My.Resources.JFLibraryErrors.Error_Try_Catch_Server, ex.Message), ex);
            }
        }


        IEnumerable<SP_GRID_TAREAS_MODEL> IProyectos.GetTareas()
        {
            try
            {
                using (SICACIEntities cnn = new SICACIEntities())
                {
                    return cnn.SP_GRID_TAREAS().ToArray();
                }
            }
            catch (Exception ex)
            {
                if (ex.InnerException is SqlException) throw ex.InnerException;
                throw new Exception(string.Format("{0} {1}", JertiFramework.My.Resources.JFLibraryErrors.Error_Try_Catch_Server, ex.Message), ex);
            }
        }


        void IProyectos.CrearTarea(int IDProyecto, int Orden, string Titulo, string Descripcion, string Responsable, string Recursos, DateTime FechaFin, decimal Progreso, string PersonalInvolucrado, string UserCreador)
        {
            try
            {
                using (SICACIEntities cnn = new SICACIEntities())
                {
                    cnn.SP_CREAR_TAREA(IDProyecto, Orden, Titulo, Descripcion, Responsable, Recursos, FechaFin,
                        Progreso, UserCreador, PersonalInvolucrado);
                }
            }
            catch (Exception ex)
            {
                if (ex.InnerException is SqlException) throw ex.InnerException;
                throw new Exception(string.Format("{0} {1}", JertiFramework.My.Resources.JFLibraryErrors.Error_Try_Catch_Server, ex.Message), ex);
            }
        }


        SP_CONSULTAR_TAREA_INFO_MODEL IProyectos.ConsultarInfo_Tarea(int id)
        {
            try
            {
                using (SICACIEntities cnn = new SICACIEntities())
                {
                    return cnn.SP_CONSULTAR_TAREA_INFO().Where(ti => ti.ID_TAREA.Equals(id)).FirstOrDefault();
                }
            }
            catch (Exception ex)
            {
                if (ex.InnerException is SqlException) throw ex.InnerException;
                throw new Exception(string.Format("{0} {1}", JertiFramework.My.Resources.JFLibraryErrors.Error_Try_Catch_Server, ex.Message), ex);
            }
        }


        IEnumerable<SP_CONSULTAR_TAREA_PERSONAL_MODEL> IProyectos.ConsultarPersonal_Tarea(int id)
        {
            try
            {
                using (SICACIEntities cnn = new SICACIEntities())
                {
                    return cnn.SP_CONSULTAR_TAREA_PERSONAL().Where(ti => ti.ID_TAREA.Equals(id)).ToArray();
                }
            }
            catch (Exception ex)
            {
                if (ex.InnerException is SqlException) throw ex.InnerException;
                throw new Exception(string.Format("{0} {1}", JertiFramework.My.Resources.JFLibraryErrors.Error_Try_Catch_Server, ex.Message), ex);
            }
        }


        IEnumerable<SP_CONSULTAR_TAREA_ARCHIVOS_MODEL> IProyectos.ConsultarArchivos_Tarea(int id)
        {
            try
            {
                using (SICACIEntities cnn = new SICACIEntities())
                {
                    return cnn.SP_CONSULTAR_TAREA_ARCHIVOS().Where(ti => ti.ID_TAREA.Equals(id)).ToArray();
                }
            }
            catch (Exception ex)
            {
                if (ex.InnerException is SqlException) throw ex.InnerException;
                throw new Exception(string.Format("{0} {1}", JertiFramework.My.Resources.JFLibraryErrors.Error_Try_Catch_Server, ex.Message), ex);
            }
        }


        string IProyectos.VincularArchivo_Tarea(int id, string titulo, string extensionFile, string usuario)
        {
            try
            {
                using (SICACIEntities cnn = new SICACIEntities())
                {
                    return cnn.SP_CREAR_TAREA_ARCHIVO(id, titulo, extensionFile, usuario).FirstOrDefault();
                }
            }
            catch (Exception ex)
            {
                if (ex.InnerException is SqlException) throw ex.InnerException;
                throw new Exception(string.Format("{0} {1}", JertiFramework.My.Resources.JFLibraryErrors.Error_Try_Catch_Server, ex.Message), ex);
            }
        }


        IEnumerable<SP_CONSULTAR_TAREA_COMENTARIOS_BYID_MODEL> IProyectos.ConsultarComentarios_Tarea(int id)
        {
            try
            {
                using (SICACIEntities cnn = new SICACIEntities())
                {
                    return cnn.SP_CONSULTAR_TAREA_COMENTARIOS_BYID(id).ToArray();
                }
            }
            catch (Exception ex)
            {
                if (ex.InnerException is SqlException) throw ex.InnerException;
                throw new Exception(string.Format("{0} {1}", JertiFramework.My.Resources.JFLibraryErrors.Error_Try_Catch_Server, ex.Message), ex);
            }
        }


        void IProyectos.CrearComentario_Tarea(int id, string comentario, string user)
        {
            try
            {
                using (SICACIEntities cnn = new SICACIEntities())
                {
                    cnn.SP_CREAR_TAREA_COMENTARIO(id, comentario, user);
                }
            }
            catch (Exception ex)
            {
                if (ex.InnerException is SqlException) throw ex.InnerException;
                throw new Exception(string.Format("{0} {1}", JertiFramework.My.Resources.JFLibraryErrors.Error_Try_Catch_Server, ex.Message), ex);
            }
        }


        void IProyectos.ModificarTarea(int IDTarea, int Orden, string Titulo, string Descripcion, string Responsable, string Recursos, DateTime FechaFin, decimal Progreso, string PersonalInvolucrado, string UserCreador)
        {
            try
            {
                using (SICACIEntities cnn = new SICACIEntities())
                {
                    cnn.SP_MODIFICAR_TAREA(IDTarea, Orden, Titulo, Descripcion, Responsable, Recursos,
                        FechaFin, Progreso, UserCreador, PersonalInvolucrado);
                }
            }
            catch (Exception ex)
            {
                if (ex.InnerException is SqlException) throw ex.InnerException;
                throw new Exception(string.Format("{0} {1}", JertiFramework.My.Resources.JFLibraryErrors.Error_Try_Catch_Server, ex.Message), ex);
            }
        }
    

        void IProyectos.EliminarTarea(int IDTarea, string User)
        {
 	        try
            {
                using (SICACIEntities cnn = new SICACIEntities())
                {
                    cnn.SP_ELIMINAR_TAREA(IDTarea, User);
                }
            }
            catch (Exception ex)
            {
                if (ex.InnerException is SqlException) throw ex.InnerException;
                throw new Exception(string.Format("{0} {1}", JertiFramework.My.Resources.JFLibraryErrors.Error_Try_Catch_Server, ex.Message), ex);
            }
}


        void IProyectos.EliminarArchivo_Tarea(int IDTarea, string fileName, string User)
        {
            try
            {
                using (SICACIEntities cnn = new SICACIEntities())
                {
                    cnn.SP_ELIMINAR_ARCHIVO_DE_TAREA(IDTarea, fileName, User);
                }
            }
            catch (Exception ex)
            {
                if (ex.InnerException is SqlException) throw ex.InnerException;
                throw new Exception(string.Format("{0} {1}", JertiFramework.My.Resources.JFLibraryErrors.Error_Try_Catch_Server, ex.Message), ex);
            }
        }


        IEnumerable<SP_CONSULTAR_TAREA_INFO_MODEL> IProyectos.ConsultarTareas()
        {
            try
            {
                using (SICACIEntities cnn = new SICACIEntities())
                {
                    return cnn.SP_CONSULTAR_TAREA_INFO().ToArray();
                }
            }
            catch (Exception ex)
            {
                if (ex.InnerException is SqlException) throw ex.InnerException;
                throw new Exception(string.Format("{0} {1}", JertiFramework.My.Resources.JFLibraryErrors.Error_Try_Catch_Server, ex.Message), ex);
            }
        }


        IEnumerable<SP_GRID_PROYECTOS_MODEL> IProyectos.GetGridData_ByUser(string user)
        {
            try
            {
                using (SICACIEntities cnn = new SICACIEntities())
                {
                    return cnn.SP_GRID_PROYECTOS_BYUSER(user).ToArray();
                }
            }
            catch (Exception ex)
            {
                if (ex.InnerException is SqlException) throw ex.InnerException;
                throw new Exception(string.Format("{0} {1}", JertiFramework.My.Resources.JFLibraryErrors.Error_Try_Catch_Server, ex.Message), ex);
            }
        }


        IEnumerable<SP_GRID_TAREAS_MODEL> IProyectos.GetTareas_ByUser(string user, int ID)
        {
            try
            {
                using (SICACIEntities cnn = new SICACIEntities())
                {
                    return cnn.SP_GRID_TAREAS_BYUSER(user, ID).ToArray();
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