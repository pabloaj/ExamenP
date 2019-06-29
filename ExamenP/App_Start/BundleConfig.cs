﻿using System.Web;
using System.Web.Optimization;

namespace ExamenP
{
    public class BundleConfig
    {
        // For more information on bundling, visit https://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at https://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));
            ////
            ///
            bundles.Add(new ScriptBundle("~/bundles/extensions").Include(
                "~/Scripts/DataTables/jquery.dataTables.js",
                    "~/Scripts/moment.min.js",
                         "~/Scripts/DataTables/dataTables.bootstrap.min.js",
                         "~/Scripts/DataTables/dataTables.buttons.min.js",
                       "~/Scripts/DataTables/buttons.bootstrap.min.js",
                         "~/Scripts/DataTables/jszip.min.js",
                         "~/Scripts/DataTables/buttons.flash.min.js",
                         "~/Scripts/DataTables//buttons.colVis.min.js",
                       "~/Scripts/DataTables/buttons.html5.min.js",
                       "~/Scripts/DataTables/dataTables.colReorder.min.js",
                        "~/Scripts/DataTables/dataTables.select.min.js",
                    
               
                       "~/Scripts/DataTables/dataTables.fixedHeader.min.js"));
            ////
            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css",
                      "~/Content/site.css"));
        }
    }
}