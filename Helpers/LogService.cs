using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Documents;
using System.Windows.Controls;
using System.Windows.Media;




namespace Sampler.Helpers {
            public class LogService
            {
                    private readonly RichTextBox _target;
    

                    public LogService(RichTextBox target)   {   _target = target;   }

       
                    public void Append(string message)
                    {
                        string clean = StripPrefix(message, out Brush color);

                        var paragraph = new Paragraph(
                            new Run(clean) { Foreground = color });

                        _target.Document.Blocks.Add(paragraph);
                        _target.ScrollToEnd();
                    }


                    private string StripPrefix(string line, out Brush color)   {
                            if (line.StartsWith("[INFO]"))
                            {
                                color = Brushes.Blue;
                                return line.Substring("[INFO]".Length).TrimStart();
                            }
                            if (line.StartsWith("[ERROR]"))
                            {
                                color = Brushes.Red;
                                return line.Substring("[ERROR]".Length).TrimStart();
                            }
                            if (line.StartsWith("[DEBUG]"))
                            {
                                color = Brushes.Green;
                                return line.Substring("[DEBUG]".Length).TrimStart();
                            }

                            color = Brushes.Black;
                            return line;
                    }
            }
}
