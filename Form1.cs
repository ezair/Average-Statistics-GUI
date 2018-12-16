/// Eric Zair
/// Form.cs
/// This program builds a gui to calculate mean, median, and mode of a given list of numbers
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Statistics
{
    public partial class Form1 : Form
    {
        //variables.
        private List<double> list = new List<double>();

        public Form1()
        {
            InitializeComponent();
        }

        //Whole Form.
        private void Form1_Load(object sender, EventArgs e)
        {
            
        }

        //This clears the textBoxResult and 
        //Prints out the mean, median, and mode.
        private void calculate_Click(object sender, EventArgs e)
        {
            if (list.Count >= 2)
            {
                textBoxResult.Text = "Mean: " + Convert.ToString(mean());
                textBoxResult.AppendText(Environment.NewLine);
                textBoxResult.Text += "Median: " + Convert.ToString(median());
                textBoxResult.AppendText(Environment.NewLine);

                //there is mode if number only appears one time.
                if (mode() < 2)
                    textBoxResult.Text += "No Mode.";
                else
                    textBoxResult.Text += "Mode: " + Convert.ToString(mode());
                //clear the list after.
                list.Clear();

                //space to make sure that numbers 
                textBoxResult.AppendText(Environment.NewLine);
            }
            else
                MessageBox.Show("Must be At Least Two Numbers.", "Must Enter a Number", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
        }

        //COnv
        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                //add user input to list.
                list.Add(Convert.ToDouble(textBoxEnter.Text));
                //add user input to textBox.
                textBoxResult.AppendText(textBoxEnter.Text);
                textBoxResult.AppendText(Environment.NewLine);
                textBoxEnter.Text = "";
            }
            catch
            {
                //error message if try is not successful 
                //displays a popUpError.
                MessageBox.Show("Error: Not A Number!", "Must Enter a Number", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void textBoxEnter_Enter(object sender, EventArgs e)
        {
            ActiveForm.AcceptButton = button1;
           
        }

        private void button2_Click(object sender, EventArgs e)
        {
            //remove all that user entered
            list.Clear();
            textBoxResult.Clear();
            //clear the text inside of textBoxEnter.
            textBoxEnter.Clear();
        }

        //This method returns the median in a list.
        private double median()
        {
            //Sort list before preforming any further operations.
            list.Sort();
            
            //If the list is even return the list at the exact middle.
            if (list.Count % 2 == 1)
                return list[list.Count / 2];
            //Otherwise return the list at the middle + the list at the middle+1
            //and divide by 2.
            else
            {
                double num1 = list[list.Count / 2];
                double num2 = list[(list.Count / 2) - 1];
                return (num1 + num2) / 2;
            }
        }

        //This method returns the mean of data in list.
        //Mean is the sum of all values in the list / number of Values.
        private double mean()
        {
            double sum = 0;
            foreach (double value in list)
                sum += value;
            return sum / list.Count;
        }

        //This method returns the highest occuring number with the
        //highest value.
        private double mode()
        {   
            //Dictionary to hold the number of values for a key.
            Dictionary<double, int> dict = new Dictionary<double, int>();
            double mode = 0.0;
            int max = 0;
            foreach (double key in list)
            {
                if (!dict.ContainsKey(key))
                    dict.Add(key, 1);
                else
                    dict[key] += 1;

                if (dict[key] > max)
                    max = dict[key];
            }

            //if the max is less than 2 return it right away to indicate an error.
            if (max < 2)
                return max;

            SortedSet<double> keys = new SortedSet<double>(dict.Keys);
            foreach (double key in keys)
                if (dict[key] == max)
                    mode = key;
            return mode;
        }
        

        //return a given list in the form of a string
        private string listToString(List<double> list)
        {
            if (list.Count < 1)
                return "[]";
            else
            {
                string str = "[" + list[0];
                for (int i = 1; i < list.Count; i++)
                    str += ", " + list[i];
                return str + "]";
            }
        }
    }
}
