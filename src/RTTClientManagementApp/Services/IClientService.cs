using System.ServiceModel;

using RTTClientManagementApp.Models;


namespace RTTClientManagementApp.Services
{
    [ServiceContract]
    public interface IClientService
    {
        [OperationContract]
        void AddClient(ClientModel client);

        [OperationContract]
        List<ClientModel> GetClients();

        [OperationContract]
        void ExportClients(string filePath);
    }
}