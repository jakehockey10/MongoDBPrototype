using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MongoDBPrototype.Properties;

namespace MongoDBPrototype
{
    public partial class Form1 : Form
    {
        //private void button1_Click(object sender, EventArgs e)
        //{
        //    var eventViewerReader = new EventViewerReader();
        //    eventViewerReader.ReadEvents();
        //}

        private readonly MongoDBWrapper _mongoWrapper;

        public enum Action
        {
            NoOp,
            CountRestaurants
        }

        private readonly Dictionary<Action, string> _comboBoxEntries = new Dictionary<Action, string>
        {
            {Action.CountRestaurants, "Count Restaurants"}
        };

        public Form1()
        {
            InitializeComponent();
            _mongoWrapper = new MongoDBWrapper();
            StartMongoWithOutput();
        }

        private void StartMongoWithOutput()
        {
            if (!_mongoWrapper.IsServerRunning) ConsoleOutputter.OutputResultsOf(() => _mongoWrapper.StartServer());
            if (_mongoWrapper.IsServerRunning) ConsoleOutputter.OutputResultsOf(() => _mongoWrapper.ConnectToTestDB());
        }

        private async void button1_Click(object sender, EventArgs e)
        {
            switch (SelectedItem.Key)
            {
                case Action.NoOp:
                    break;
                case Action.CountRestaurants:
                    await CountRestaurants();
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        public KeyValuePair<Action, string> SelectedItem => (KeyValuePair<Action, string>)comboBox1.SelectedItem;

        private async Task CountRestaurants()
        {
            label1.Text = Resources.Form1_CountRestaurants_working___;
            string output = await TaskCompletionTimeRecorder.RecordTaskCompletionTime(() => _mongoWrapper.CountRestaurants());
            label1.Text = output;
        }

        private async Task QueryByTopLevelField(string field, string value)
        {
            await ConsoleOutputter.OutputResultsOf(() => _mongoWrapper.QueryByTopLevelField(field, value));
        }

        private async Task QueryByAFieldInAnEmbeddedDocument(string field, string value)
        {
            await ConsoleOutputter.OutputResultsOf(() => _mongoWrapper.QueryByAFieldInAnEmbeddedDocument(field, value));
        }

        private async Task QueryByAFieldInAnArray(string field, string value)
        {
            await ConsoleOutputter.OutputResultsOf(() => _mongoWrapper.QueryByAFieldInAnArray(field, value));
        }

        private async Task GreaterThanOperator(string field, int value)
        {
            await ConsoleOutputter.OutputResultsOf(() => _mongoWrapper.GreaterThanOperator(field, value));
        }

        private async Task LessThanOperator(string field, int value)
        {
            await ConsoleOutputter.OutputResultsOf(() => _mongoWrapper.LessThanOperator(field, value));
        }

        private async Task LogicalAnd(List<Tuple<string, string>> fieldValuePairs)
        {
            await ConsoleOutputter.OutputResultsOf(() => _mongoWrapper.LogicalAnd(fieldValuePairs));
        }

        private async Task LogicalOr(List<Tuple<string, string>> fieldValuePairs)
        {
            await ConsoleOutputter.OutputResultsOf(() => _mongoWrapper.LogicalOr(fieldValuePairs));
        }

        private async Task SortQueryResults()
        {
            await ConsoleOutputter.OutputResultsOf(() => _mongoWrapper.SortQueryResults());
        }

        private async Task UpdateTopLevelFields()
        {
            await ConsoleOutputter.OutputResultsOf(() => _mongoWrapper.UpdateTopLevelFields());
        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            _mongoWrapper.KillServerAndCleanup();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            comboBox1.DataSource = _comboBoxEntries.ToList();
            comboBox1.DisplayMember = "Value";
            comboBox1.ValueMember = "Key";
        }

        private void comboBox1_SelectionChangeCommitted(object sender, EventArgs e)
        {
        }
    }
}
