class Program
{
    static void Main(string[] args)
    {
        var clientService = new ClientServiceClient(); // Assuming ClientServiceClient is the WCF client proxy

        while (true)
        {
            Console.WriteLine("1. Add Client");
            Console.WriteLine("2. List Clients");
            Console.WriteLine("3. Export Clients");
            Console.WriteLine("4. Exit");
            Console.Write("Select an option: ");
            var choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    AddClient(clientService);
                    break;
                case "2":
                    ListClients(clientService);
                    break;
                case "3":
                    ExportClients(clientService);
                    break;
                case "4":
                    return;
                default:
                    Console.WriteLine("Invalid option. Please try again.");
                    break;
            }
        }
    }

    static void AddClient(ClientServiceClient clientService)
    {
        try
        {
            Console.Write("Enter client name: ");
            var name = Console.ReadLine();

            Console.Write("Enter gender: ");
            var gender = Console.ReadLine();

            var client = new Client
            {
                Name = name,
                Gender = gender,
                Addresses = new List<Address>(),
                ContactInfos = new List<ContactInfo>()
            };

            // Add address
            Console.Write("Enter address type (e.g., Residential): ");
            var addressType = Console.ReadLine();
            Console.Write("Enter street: ");
            var street = Console.ReadLine();
            Console.Write("Enter city: ");
            var city = Console.ReadLine();
            Console.Write("Enter state: ");
            var state = Console.ReadLine();
            Console.Write("Enter zip code: ");
            var zipCode = Console.ReadLine();

            client.Addresses.Add(new Address
            {
                Type = addressType,
                Street = street,
                City = city,
                State = state,
                ZipCode = zipCode
            });

            // Add contact info
            Console.Write("Enter contact type (e.g., Cell Number): ");
            var contactType = Console.ReadLine();
            Console.Write("Enter contact value: ");
            var contactValue = Console.ReadLine();

            client.ContactInfos.Add(new ContactInfo
            {
                Type = contactType,
                Value = contactValue
            });

            clientService.AddClient(client);
            Console.WriteLine("Client added successfully.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error adding client: {ex.Message}");
        }
    }

    static void ListClients(ClientServiceClient clientService)
    {
        try
        {
            var clients = clientService.GetClients();
            foreach (var client in clients)
            {
                Console.WriteLine($"ID: {client.Id}, Name: {client.Name}, Gender: {client.Gender}");
                foreach (var address in client.Addresses)
                {
                    Console.WriteLine($"  Address: {address.Type}, {address.Street}, {address.City}, {address.State}, {address.ZipCode}");
                }
                foreach (var contact in client.ContactInfos)
                {
                    Console.WriteLine($"  Contact: {contact.Type}, {contact.Value}");
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error listing clients: {ex.Message}");
        }
    }

    static void ExportClients(ClientServiceClient clientService)
    {
        try
        {
            Console.Write("Enter file path to export: ");
            var filePath = Console.ReadLine();
            clientService.ExportClients(filePath);
            Console.WriteLine("Clients exported successfully.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error exporting clients: {ex.Message}");
        }
    }
}