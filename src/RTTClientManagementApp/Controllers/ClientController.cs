using Microsoft.AspNetCore.Mvc;
using RTTClientManagementApp.Models;
using RTTClientManagementApp.Services;
using System.Threading.Tasks;

namespace RTTClientManagementApp.Controllers
{
    public class ClientController : Controller
    {
        private readonly IClientService _clientService;

        public ClientController(IClientService clientService)
        {
            _clientService = clientService;
        }

        public IActionResult Index()
        {
            // Fetch clients from the service
            return View();
        }

        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Add(ClientModel client)
        {
            try
            {
                await _clientService.AddClientAsync(client);
                return RedirectToAction("Index");
            }
            catch (ArgumentException ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
                return View(client);
            }
        }

        public IActionResult Export()
        {
            var filePath = "clients.csv";
            ExportClients(filePath);
            return File(System.IO.File.ReadAllBytes(filePath), "text/csv", "clients.csv");
        }

        private void ExportClients(string filePath)
        {
            // Implement export logic
            var csvLines = new List<string> { "Id,Name,Gender,AddressType,Street,City,State,ZipCode" };

            // Example: Iterate over clients and addresses
            foreach (var client in new List<ClientModel>()) // Replace with actual client list
            {
                foreach (var address in client.Addresses)
                {
                    csvLines.Add($"{client.Id},{client.Name},{client.Gender},{address.Type},{address.Street},{address.City},{address.State},{address.ZipCode}");
                }
            }

            System.IO.File.WriteAllLines(filePath, csvLines);
        }
    }
}