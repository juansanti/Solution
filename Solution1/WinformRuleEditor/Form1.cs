using Sigs.AutorizacionesOnline.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Workflow.Activities.Rules;
using System.Workflow.Activities.Rules.Design;
using System.Workflow.ComponentModel.Serialization;
using System.Xml;

namespace WinformRuleEditor
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        public string Directory
        {
            get
            {
                return @"C:\Users\jsanti\Documents\Visual Studio 2012\Projects\Prestamos\Solution\Solution1\Autorizaciones.Domain\Rules\";
            }
        }

        string GetFullFileName(string file)
        {
            return Directory + file;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            EditarReglasAutorizaciones(typeof(Autorizacion), Autorizacion.RuleSetFileName);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            EditarReglasAutorizaciones(typeof(PrestacionAutorizacion), PrestacionAutorizacion.RuleSetFileName);
        }

        private void EditarReglasAutorizaciones(Type type, string fileName)
        {
            // Rules file name to serialize to and deserialize from

            RuleSet ruleSet = null;

            WorkflowMarkupSerializer serializer = new WorkflowMarkupSerializer();

            if (Existe(fileName))
            {
                ruleSet = GetRuleSetFromFile(fileName);
            }

            // Create a RuleSet that works with Orders (just another .net Object)
            RuleSetDialog ruleSetDialog = new RuleSetDialog(type, null, ruleSet);

            // Show the RuleSet Editor
            DialogResult result = ruleSetDialog.ShowDialog();

            if (result == DialogResult.OK)
            {
                ruleSet = ruleSetDialog.RuleSet;
                // Serialize to a .rules file

                XmlWriter rulesWriter = XmlWriter.Create(fileName);
                serializer.Serialize(rulesWriter, ruleSet);
                rulesWriter.Close();
            }
        }

        public RuleSet GetRuleSetFromFile(string fileName)
        {
            WorkflowMarkupSerializer serializer = new WorkflowMarkupSerializer();

            XmlTextReader rulesReader = new XmlTextReader(fileName);

            var ruleSet = (RuleSet)serializer.Deserialize(rulesReader);
            rulesReader.Close();

            return ruleSet;
        }

        private bool Existe(string path)
        {
            return File.Exists(path);
        }

        private void btnExaminar_Click(object sender, EventArgs e)
        {
            openFileDialog1.InitialDirectory = @"C:\Users\jsanti\Documents\Visual Studio 2012\Projects\Prestamos\Solution\Solution1\Autorizaciones.Domain\Rules\";


            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                textBox1.Text = openFileDialog1.FileName;
            }
        }
    }
}
