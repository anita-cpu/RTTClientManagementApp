public class ClientRepository
{
    private List<Client> clients = new List<Client>();

    public IEnumerable<Client> GetAllClients()
    {
        return clients;
    }

    public void AddClient(Client client)
    {
        if (client == null)
            throw new ArgumentNullException(nameof(client));
        
        CheckForDuplicateClient(client);
        clients.Add(client);
    }

    private void CheckForDuplicateClient(Client client)
    {
        if (clients.Any(c => c.Name.Equals(client.Name, StringComparison.OrdinalIgnoreCase)))
        {
            throw new InvalidOperationException($"A client with name {client.Name} already exists.");
        }
    }
}