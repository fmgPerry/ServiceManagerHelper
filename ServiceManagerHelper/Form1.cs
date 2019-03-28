using System;
using System.Windows.Forms;
using ServiceManagerHelper.Localhost_PC;
using System.Diagnostics;

namespace ServiceManagerHelper
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;
            var stopwatch = new Stopwatch();
            stopwatch.Start();

            var pcWebservice = new FMGConnectServicePortTypeClient("FMGConnectServiceSoap12Port1");
            
            pcWebservice.Endpoint.Binding.SendTimeout = TimeSpan.FromMinutes(2);// timing out in 1 min

            //var result = pcWebservice.getContactAccountAssociation(
            //    authentication: new authentication { username = "su", password = "gw" },
            //    gw_language: null,
            //    gw_locale: null,
            //    contactId: "abPROD:21785");

            //var policySummary = pcWebservice.getPolicySummary(
            //    authentication: new authentication { username = "su", password = "gw" },
            //    gw_language: null,
            //    gw_locale: null,
            //    accountNumber: "10053718");

            var vehicles = ExcelJobs.GetVehicles(@"C:\Users\fmgpr2\AppData\Local\Temp\ServiceManagerHelper\AddVehicleSample.xlsx");

            var addVehicleCriteriaArray = new AddVehicleCriteria[vehicles.Count];
            int i = 0;
            foreach(var vehicle in vehicles)
            {
                addVehicleCriteriaArray[i++] = vehicle._addVehicleCriteria;
            }
            
            pcWebservice.addMultipleVehicle(
                authentication: new authentication { username = "su", password = "gw" },
                gw_language: null,
                gw_locale: null,
                contactId: "abPROD:24071",
                policyNumber: "108521",
                vehicleCriteriaList: addVehicleCriteriaArray,
                asOfDate: DateTime.Now.ToString("yyyy-MM-dd'T'00:01:00")
                );

            stopwatch.Stop();
            Cursor.Current = Cursors.Default;
            MessageBox.Show($@"Added {vehicles.Count} vehicles in {stopwatch.Elapsed.TotalSeconds} seconds");
        }
    }
}
