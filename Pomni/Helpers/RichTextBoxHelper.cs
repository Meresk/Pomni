using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;

namespace Pomni.Helpers
{
    public class RichTextBoxHelper : DependencyObject
    {
        private static void OnTextChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is RichTextBox richTextBox)
            {
                // Очищаем текущий текст
                richTextBox.Document.Blocks.Clear();

                // Добавляем новый текст
                if (e.NewValue != null)
                {
                    richTextBox.Document.Blocks.Add(new Paragraph(new Run(e.NewValue.ToString())));
                }
            }
        }

        public static readonly DependencyProperty TextProperty =
        DependencyProperty.RegisterAttached(
            "Text",
            typeof(string),
            typeof(RichTextBoxHelper),
            new FrameworkPropertyMetadata(
                string.Empty,
                FrameworkPropertyMetadataOptions.BindsTwoWayByDefault,
                OnTextChanged));

        public static string GetText(DependencyObject obj)
        {
            return (string)obj.GetValue(TextProperty);
        }
    }
}
