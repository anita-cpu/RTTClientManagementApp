using RTTClientManagementApp.Models;
using RTTClientManagementApp.Services;

public class ClientService : IClientService
{
    private readonly ClientRepository _repository;

    public ClientService()
    {
        _repository = new ClientRepository();
    }

    public IEnumerable<ClientModel> GetAllClients()
    {
        try
        {
            return _repository.GetAllClients();
        }
        catch (Exception ex)
        {
            // Log error (consider using a logging framework library like NLog or log4net)
            Console.WriteLine($"Error fetching clients: {ex.Message}");
            throw new FaultException("An error occurred while retrieving clients.");
        }
    }

    public void AddClient(ClientModel client)
    {
        try
        {
            ValidateClient(client);
            _repository.AddClient(client);
        }
        catch (ArgumentNullException ex)
        {
            Console.WriteLine($"Validation error: {ex.Message}");
            throw new FaultException("Client data is required.");
        }
        catch (ArgumentException ex)
        {
            Console.WriteLine($"Validation error: {ex.Message}");
            throw new FaultException("Invalid client data provided.");
        }
        catch (InvalidOperationException ex)
        {
            Console.WriteLine($"Duplicate client error: {ex.Message}");
            throw new FaultException("Client already exists.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error adding client: {ex.Message}");
            throw new FaultException("An error occurred while adding the client.");
        }
    }

    private void ValidateClient(ClientModel client)
    {
        if (string.IsNullOrWhiteSpace(client.Name))
            throw new ArgumentException("Client name is required.");

        if (string.IsNullOrWhiteSpace(client.Gender))
            throw new ArgumentException("Client gender is required.");

        if (client.Addresses == null || !client.Addresses.Any())
            throw new ArgumentException("At least one address is required.");
    }

    public void ExportClients(string filePath)
    {
        try
        {
            var clients = _repository.GetAllClients();
            using (var writer = new StreamWriter(filePath))
            {
                foreach (var client in clients)
                {
                    writer.WriteLine($"{client.Name},{client.Gender}");
                    foreach (var address in client.Addresses)
                    {
                        writer.WriteLine($"{address.Type},{address.Street},{address.City},{address.State},{address.ZipCode}");
                    }
                }
            }
        }
        catch (IOException ex)
        {
            Console.WriteLine($"File writing error: {ex.Message}");
            throw new FaultException("An error occurred while exporting clients.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error exporting clients: {ex.Message}");
            throw new FaultException("An error occurred while exporting clients.");
        }
    }
}