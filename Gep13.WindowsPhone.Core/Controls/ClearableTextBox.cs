//-----------------------------------------------------------------------
// <copyright file="ClearableTextBox.cs" company="GEP13">
//      Copyright (c) GEP13, 2012. All rights reserved.
//      Permission is hereby granted, free of charge, to any person obtaining a copy of this software and associated documentation 
//      files (the “Software”), to deal in the Software without restriction, including without limitation the rights to use, copy, 
//      modify, merge, publish, distribute, sublicense, and/or sell copies of the Software, and to permit persons to whom the Software 
//      is furnished to do so, subject to the following conditions:
//
//      The above copyright notice and this permission notice shall be included in all copies or substantial portions of the Software.
//
//      THE SOFTWARE IS PROVIDED “AS IS”, WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES 
//      OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE 
//      LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN 
//      CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.
//
//      This implementation has been taken from http://jacob4u2.blogspot.co.uk/2010/07/clearable-textbox-custom-control.html and
//      adapted as required.
// </copyright>
//-----------------------------------------------------------------------

namespace Gep13.WindowsPhone.Core.Controls
{
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Data;

    /// <summary>
    /// A control to allow the clearing of it's contents
    /// </summary>
    [TemplatePart(Name = "ClearButtonTemplate", Type = typeof(Button))]
    [TemplateVisualState(Name = "Normal", GroupName = "VisualStates")]
    [TemplateVisualState(Name = "Cleared", GroupName = "VisualStates")]
    public class ClearableTextBox : Control
    {
        /// <summary>
        /// Local DependencyProperty for the Content of the ClearButton
        /// </summary>
        public static readonly DependencyProperty ClearButtonContentProperty =
            DependencyProperty.Register("ClearButtonContent", typeof(object), typeof(ClearableTextBox), new PropertyMetadata(null));

        /// <summary>
        /// Local DependencyProperty for the Template of the ClearButton
        /// </summary>
        public static readonly DependencyProperty ClearButtonTemplateProperty =
            DependencyProperty.Register("ClearButtonTemplate", typeof(ControlTemplate), typeof(ClearableTextBox), new PropertyMetadata(null));

        /// <summary>
        /// Local DependencyProperty for the Template of the EditBox
        /// </summary>
        public static readonly DependencyProperty EditBoxTemplateProperty =
            DependencyProperty.Register("EditBoxTemplate", typeof(ControlTemplate), typeof(ClearableTextBox), new PropertyMetadata(null));

        /// <summary>
        /// Local DependencyProperty for the Text Property
        /// </summary>
        public static readonly DependencyProperty TextProperty =
            DependencyProperty.Register("Text", typeof(string), typeof(ClearableTextBox), new PropertyMetadata(string.Empty));

        /// <summary>
        /// Local variable to refernce the ClearButton
        /// </summary>
        private Button clearBtn;

        /// <summary>
        /// Local variable to reference the EditBox
        /// </summary>
        private TextBox editBox;

        /// <summary>
        /// Initializes a new instance of the ClearableTextBox class
        /// </summary>
        public ClearableTextBox()
        {
            DefaultStyleKey = typeof(ClearableTextBox);
        }

        /// <summary>
        /// Gets or sets the Content for the ClearButton
        /// </summary>
        public object ClearButtonContent
        {
            get { return GetValue(ClearButtonContentProperty); }
            set { SetValue(ClearButtonContentProperty, value); }
        }

        /// <summary>
        /// Gets or sets the Template for the ClearButton
        /// </summary>
        public ControlTemplate ClearButtonTemplate
        {
            get { return (ControlTemplate)GetValue(ClearButtonTemplateProperty); }
            set { SetValue(ClearButtonTemplateProperty, value); }
        }

        /// <summary>
        /// Gets or sets the Template for the EditBox
        /// </summary>
        public ControlTemplate EditBoxTemplate
        {
            get { return (ControlTemplate)GetValue(EditBoxTemplateProperty); }
            set { SetValue(EditBoxTemplateProperty, value); }
        }

        /// <summary>
        /// Gets or sets the Text for the TextBox
        /// </summary>
        public string Text
        {
            get { return (string)GetValue(TextProperty); }
            set { SetValue(TextProperty, value); }
        }

        /// <summary>
        /// Overridden method for what to do when the Template is applied
        /// </summary>
        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            // Hook up our click handler for the clear button.
            this.clearBtn = GetTemplateChild("clearBtn") as Button;
            this.clearBtn.Click += this.ClearButton_Click;

            this.editBox = GetTemplateChild("editBox") as TextBox;

            // Bind our text box to this instances Text property.
            var b = new Binding("Text");
            b.Mode = BindingMode.TwoWay;
            b.Source = this;
            this.editBox.SetBinding(TextBox.TextProperty, b);
        }

        /// <summary>
        /// Event handler for when the ClearButton is clicked
        /// </summary>
        /// <param name="sender">The object that raised the event</param>
        /// <param name="e">The arguments passed into the event</param>
        private void ClearButton_Click(object sender, RoutedEventArgs e)
        {
            VisualStateManager.GoToState(this, "Normal", false); // An empty state, resets us for the cleared.
            VisualStateManager.GoToState(this, "Cleared", false);
            this.Text = string.Empty;
        }
    }
}