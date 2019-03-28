using ServiceManagerHelper.Localhost_PC;
using System;

namespace ServiceManagerHelper
{
    public class Vehicle
    {
        public AddVehicleCriteria _addVehicleCriteria = new AddVehicleCriteria();

        public Vehicle() { }

        public Vehicle(dynamic vehicle)
        {
            var vehicleTypeCode = vehicle.Columns[1].Value?.ToString() ?? "";
            var insuredEvent = vehicle.Columns[9].Value?.ToString() ?? "";


            _addVehicleCriteria.VehicleType = new TypeCodeSummary { Code = vehicleTypeCode, DisplayName = getVehicleDisplayName(vehicleTypeCode) };
            _addVehicleCriteria.Year = vehicle.Columns[2].Value?.ToString();
            _addVehicleCriteria.Make = vehicle.Columns[3].Value?.ToString();
            _addVehicleCriteria.Model = vehicle.Columns[4].Value?.ToString();
            _addVehicleCriteria.CCRating = vehicle.Columns[5].Value?.ToString();
            _addVehicleCriteria.EngineType = vehicle.Columns[6].Value?.ToString();
            _addVehicleCriteria.Registration = vehicle.Columns[7].Value?.ToString();
            _addVehicleCriteria.GLW = vehicle.Columns[8].Value?.ToString();
            _addVehicleCriteria.InsuredEvent = new TypeCodeSummary { Code = insuredEvent, DisplayName = getInsuredEventDisplayName(insuredEvent) };
            _addVehicleCriteria.SumInsured = vehicle.Columns[10].Value?.ToString();
            _addVehicleCriteria.LocationID = vehicle.Columns[11].Value?.ToString();

        }

        private string getVehicleDisplayName(string vehicleTypeCode)
        {
            return vehicleTypeCode == "cvu" ? "Car, Van or Ute"
                : vehicleTypeCode == "caravan" ? "Caravan"
                : vehicleTypeCode == "motorhome" ? "Motorhome"
                : vehicleTypeCode == "trailer" ? "Trailer"
                : "";
        }

        private string getInsuredEventDisplayName(string insuredEvent)
        {
            return insuredEvent == "comp" ? "Comprehensive"
                : insuredEvent == "thirdparty" ? "Third Party Only"
                : insuredEvent == "thirdparty_fire_theft" ? "Third Party, Fire & Theft"
                : "";
        }
    }
}