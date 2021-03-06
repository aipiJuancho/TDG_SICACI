﻿using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using JertiFramework.Security;
using System.Web.Optimization;
using JertiFramework;
using System.Globalization;
using System.Threading;

namespace TDG_SICACI
{
    // Nota: para obtener instrucciones sobre cómo habilitar el modo clásico de IIS6 o IIS7, 
    // visite http://go.microsoft.com/?LinkId=9394801

    public class MvcApplication : System.Web.HttpApplication
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
            filters.Add(new JFAutorizationSecurity());
        }

        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                "Default", // Nombre de ruta
                "{controller}/{action}/{id}", // URL con parámetros
                new { controller = "Home", action = "Index", id = UrlParameter.Optional } // Valores predeterminados de parámetro
            );

        }

        

        protected void Application_BeginRequest(Object sender, EventArgs e)
        {    
            CultureInfo newCulture = (CultureInfo)     
            System.Threading.Thread.CurrentThread.CurrentCulture.Clone();
            newCulture.DateTimeFormat.ShortDatePattern = "dd/MM/yyyy";
            newCulture.DateTimeFormat.DateSeparator = "-";
            Thread.CurrentThread.CurrentCulture = newCulture;
        }

        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();

            // Usar LocalDB para Entity Framework de manera predeterminada
            //Database.DefaultConnectionFactory = new SqlConnectionFactory(@"Data Source=(localdb)\v11.0; Integrated Security=True; MultipleActiveResultSets=True");

            RegisterGlobalFilters(GlobalFilters.Filters);
            RegisterRoutes(RouteTable.Routes);

            //Preparamos la minimización de los CSS en la Aplicación
            Bundle cssBundle = new Bundle("~/Content/css-sicaci", new CssMinify());
            cssBundle.Include("~/Content/themes/base/jquery-ui.css");
            cssBundle.Include("~/Content/bootstrap/css/bootstrap.min.css");
            cssBundle.IncludeDirectory("~/Content", "*.css", false);
            cssBundle.Include("~/Content/themes/base/jquery.ui.datepicker.css");
            cssBundle.Include("~/Content/bootstrap-select/bootstrap-select.css");
            BundleTable.Bundles.Add(cssBundle);

            //Preparamos la minimización de los JavaScript de la Aplicación - jQuery
            Bundle jsJQuery = new Bundle("~/Scripts/js-jQuery", new JsMinify());
            jsJQuery.Include("~/Scripts/jquery-1.9.1.js");
            jsJQuery.Include("~/Scripts/jquery-ui-1.10.4.min.js");
            jsJQuery.Include("~/Scripts/jquery.validate.js");
            jsJQuery.Include("~/Scripts/additional-methods.js");
            jsJQuery.Include("~/Scripts/jquery.validate.unobtrusive.min.js");
            jsJQuery.Include("~/Scripts/jquery.validate.unobtrusive.ext.js");
            jsJQuery.Include("~/Scripts/jquery.validate.unobtrusive.bootstrap.tooltip.js");
            jsJQuery.Include("~/Scripts/jquery.jerti-1.0.0.js");
            jsJQuery.Include("~/Scripts/jquery.blockUI.js");
            jsJQuery.Include("~/Scripts/jquery.gritter.min.js");
            BundleTable.Bundles.Add(jsJQuery);

            Bundle jsJQuery_bsSelect = new Bundle("~/Scripts/js-jQuery-bsSelect", new JsMinify());
            jsJQuery_bsSelect.IncludeDirectory("~/Scripts/bootstrap-select", "*.js", false);
            BundleTable.Bundles.Add(jsJQuery_bsSelect);


            //Preparamos la minimización de los JavaScript de la Aplicación - BootStrap
            Bundle jsBootstrap = new Bundle("~/Scripts/js-BootStrap", new JsMinify());
            jsBootstrap.Include("~/Scripts/bootstrap/bootstrap.min.js");
            jsBootstrap.Include("~/Scripts/respond.min.js");
            jsBootstrap.Include("~/Scripts/bsGrid/jquery.bs_pagination.js");
            jsBootstrap.Include("~/Scripts/bsGrid/jquery.bs_pagination.en.js");
            BundleTable.Bundles.Add(jsBootstrap);

            //Preparamos el script de validación para vistas parciales
            Bundle jsPartialViews = new Bundle("~/Scripts/js-partialview", new JsMinify());
            //jsPartialViews.Include("~/Scripts/jquery-1.9.1.js");
            //jsPartialViews.Include("~/Scripts/jquery-ui-1.10.4.min.js");
            jsPartialViews.Include("~/Scripts/jquery.validate.unobtrusive.min.js");
            jsPartialViews.Include("~/Scripts/jquery.validate.unobtrusive.ext.js");
            jsPartialViews.Include("~/Scripts/jquery.validate.unobtrusive.bootstrap.tooltip.js");
            BundleTable.Bundles.Add(jsPartialViews);


            //Registramos el ModelMetadataProvider de Jerti para los Metadatos de los Modelos
            ModelMetadataProviders.Current = new JFModelMetadataProvider(); 
        }

        public override string GetVaryByCustomString(HttpContext context, string arg)
        {
            if (arg == "User")
            {
                // depends on your authentication mechanism
                return "User=" + context.User.Identity.Name;
            }

            return base.GetVaryByCustomString(context, arg);
        }
    }
}