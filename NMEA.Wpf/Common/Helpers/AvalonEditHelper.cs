using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ICSharpCode.AvalonEdit;
using Microsoft.Xaml.Behaviors;
using System.Windows;

namespace NMEA.Wpf.Common.Helpers
{
    public sealed class AvalonEditHelper : Behavior<TextEditor>
    {
        public static readonly DependencyProperty BindTextProperty =
            DependencyProperty.Register("BindText", typeof(string), typeof(AvalonEditHelper),
            new FrameworkPropertyMetadata(default(string), FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, PropertyChangedCallback));

        public String BindText
        {
            get { return (string)GetValue(BindTextProperty); }
            set { SetValue(BindTextProperty, value); }
        }

        protected override void OnAttached()
        {
            base.OnAttached();
            if (AssociatedObject != null)
                AssociatedObject.TextChanged += AssociatedObjectOnTextChanged;
        }

        protected override void OnDetaching()
        {
            base.OnDetaching();
            if (AssociatedObject != null)
                AssociatedObject.TextChanged -= AssociatedObjectOnTextChanged;
        }

        private void AssociatedObjectOnTextChanged(object sender, EventArgs eventArgs)
        {
            var textEditor = sender as TextEditor;
            if (textEditor != null)
            {
                if (textEditor.Document != null)
                    BindText = textEditor.Document.Text;
            }
        }

        private static void PropertyChangedCallback(
            DependencyObject dependencyObject,
            DependencyPropertyChangedEventArgs dependencyPropertyChangedEventArgs)
        {
            var behavior = dependencyObject as AvalonEditHelper;
            if (behavior.AssociatedObject != null)
            {
                var editor = behavior.AssociatedObject as TextEditor;
                if (editor.Document != null)
                {
                    // var caretOffset = editor.CaretOffset;
                    editor.Document.Text = dependencyPropertyChangedEventArgs.NewValue.ToString();
                    // editor.CaretOffset = caretOffset;
                }
            }
        }
    }
}
