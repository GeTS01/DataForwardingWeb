using Newtonsoft.Json;
using System.Collections;
using System.Text;

namespace Service.Helper
{
    public class HTMLHelper
    {
        public static string ToHtmlTable(IEnumerable enums)
        {
            return ToHtmlTableConverter(enums);
        }

        public static string ToHtmlTable(System.Data.DataTable dataTable)
        {
            return ConvertDataTableToHTML(dataTable);
        }

        private static string ToHtmlTableConverter(object enums)
        {
            var jsonStr = JsonConvert.SerializeObject(enums);
            var data = JsonConvert.DeserializeObject<System.Data.DataTable>(jsonStr);
            var html = ConvertDataTableToHTML(data);
            return html;
        }

        private static string ConvertDataTableToHTML(System.Data.DataTable dt)
        {
            var html = new StringBuilder("<table>");

            //Header
            html.Append("<thead><tr>");
            for (int i = 0; i < dt.Columns.Count; i++)
                html.Append("<th>" + dt.Columns[i].ColumnName + "</th>");
            html.Append("</tr></thead>");

            //Body
            html.Append("<tbody>");
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                html.Append("<tr>");
                for (int j = 0; j < dt.Columns.Count; j++)
                    html.Append("<td>" + dt.Rows[i][j].ToString() + "</td>");
                html.Append("</tr>");
            }

            html.Append("</tbody>");
            html.Append("</table>");
            return html.ToString();
        }
    }
}
