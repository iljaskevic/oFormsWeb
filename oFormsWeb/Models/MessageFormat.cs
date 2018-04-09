using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace oFormsWeb.Models
{
    public class MessageFormat
    {
        public MessageFormat()
        {
            // TODO: Change to lead a 'Default' from some config
            MessageTemplate = "<html><body><tr style='background: #eee;'><td><strong>Field</strong> </td><td>Value</td></tr>{Fields}</table></body></html>";
            FieldTemplate = "<tr><td style='vertical-align:top;'><strong>{Name}:</strong> </td><td>{Value}</td></tr>";
        }
        public string MessageTemplate { get; set; }
        public string FieldTemplate { get; set; }
    }
}
