using System.Web;
using System.Web.Optimization;

namespace CapaPresentacionAdmin
{
    public class BundleConfig
    {
        // For more information on bundling, visit https://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));
            bundles.Add(new ScriptBundle("~/bundles/complementos").Include(
                "~/Scripts/DataTables/jquery.dataTable.js",
                 "~/Scripts/DataTables/dataTable.responsive.js",
                 "~/Scripts/fontawesome/all.min.js",
                        "~/Scripts/sb-admin-2.js"));

            //   bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
            //             "~/Scripts/jquery.validate*"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at https://modernizr.com to pick only the tests you need.
            //bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
            //          "~/Scripts/modernizr-*"));

            bundles.Add(new Bundle("~/bundles/bootstrap").Include(
     "~/Scripts/bootstrap.bundle.js"));


            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.min.css",
                      "~/Content/site.css",
                       "~/Content/DataTables/css/jquery.dataTables.css",
                       "~/Content/DataTables/css/responsive.dataTables.css"));
        }
    }
}
