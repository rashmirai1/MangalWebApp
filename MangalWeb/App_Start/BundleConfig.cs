using System.Web;
using System.Web.Optimization;

namespace MangalWeb
{
    public class BundleConfig
    {
        // For more information on bundling, visit https://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"
                        ));

            //bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
            //            "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at https://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.min.js"));

            bundles.Add(new ScriptBundle("~/bundles/mangaljquery").Include(
                      "~/Scripts/jquery-3.3.1.min.js"));

            bundles.Add(new ScriptBundle("~/bundles/unobtrusiveAjaxjs").Include(
                      "~/Scripts/jquery.unobtrusive-ajax.min.js"));

            bundles.Add(new ScriptBundle("~/bundles/jquerydataTablesJS").Include(
                   "~/Content/DataTables/js/jquery.dataTables.min.js",
                   "~/Content/DataTables/js/dataTables.bootstrap.min.js"
                   ));

            bundles.Add(new ScriptBundle("~/bundles/presanction").Include(
                   "~/Scripts/Custom/PreSanction.js"));

            bundles.Add(new StyleBundle("~/Content/dataTablescss").Include(
                     "~/Content/DataTables/css/dataTables.bootstrap.min.css"
                     ));

            bundles.Add(new StyleBundle("~/Content/mangalcss").Include(
                   "~/Content/bootstrap.min.css",
                    "~/Content/Font/css/font-awesome.css",
                   "~/Content/assets/js/utility/highlight/styles/googlecode.css",
                   "~/Content/vendor/plugins/datepicker/css/bootstrap-datetimepicker.min.css",
                   "~/Content/vendor/plugins/daterange/daterangepicker.css",
                   "~/Content/vendor/plugins/colorpicker/css/bootstrap-colorpicker.min.css",
                   "~/Content/vendor/plugins/tagmanager/tagmanager.css",
                   "~/Content/assets/skin/default_skin/css/theme.css",
                   "~/Content/assets/skin/default_skin/css/responsive.css",
                   "~/Content/assets/admin-tools/admin-forms/css/admin-forms.css",
                   "~/Content/assets/my-style.css",
                   "~/Content/Site.css"
                   ));


            bundles.Add(new ScriptBundle("~/bundles/mangaljs").Include(
                   "~/Content/vendor/jquery/jquery_ui/jquery-ui.min.js",
                    "~/Content/vendor/plugins/highcharts/highcharts.js",
                     "~/Content/vendor/plugins/circles/circles.js",
                      "~/Content/vendor/plugins/raphael/raphael.js",
                      "~/Content/assets/js/bootstrap/holder.min.js",
                      "~/Content/assets/js/utility/utility.js",
                      "~/Content/assets/js/main.js",
                      "~/Content/assets/js/demo.js",
                      "~/Content/assets/admin-tools/admin-plugins/admin-panels/json2.js",
                      "~/Content/assets/admin-tools/admin-plugins/admin-panels/jquery.ui.touch-punch.min.js",
                       "~/Content/assets/admin-tools/admin-plugins/admin-panels/adminpanels.js",
                        "~/Content/assets/js/pages/widgets.js",
                         "~/Scripts/Mangal/bootbox.min.js",
                         "~/Scripts/Mangal/Comman.js"
                   ));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css",
                      "~/Content/DataTables/css/dataTables.bootstrap.min.css",
                      "~/Content/site.css"));

        }
    }
}
