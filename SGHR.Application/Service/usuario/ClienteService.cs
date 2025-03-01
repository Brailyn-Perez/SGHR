
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using SGHR.Application.DTos.usuario.Cliente;
using SGHR.Application.Interfaces.usuario;
using SGHR.Domain.Base;
using SGHR.Persistence.Interfaces.usuario;

namespace SGHR.Application.Service.usuario
{
    public class ClienteService : IClienteService
    {
        private readonly IClienteRepository _clienteRepository;
        private readonly ILogger<ClienteService> _logger;
        private readonly IConfiguration _configuraicon;

        public ClienteService(IClienteRepository clienteRepository,
            ILogger<ClienteService> logger,
            IConfiguration configuraicon)
        {
            _clienteRepository = clienteRepository;
            _logger = logger;
            _configuraicon = configuraicon;
        }
        public Task<OperationResult> GeAll()
        {
            throw new NotImplementedException();
        }

        public Task<OperationResult> GeById(int id)
        {
            throw new NotImplementedException();
        }

        public Task<OperationResult> Remove(UpdateClienteDTo dto)
        {
            throw new NotImplementedException();
        }

        public Task<OperationResult> Save(SaveClienteDTo dto)
        {
            throw new NotImplementedException();
        }

        public Task<OperationResult> Update(RemoveClienteDTo dto)
        {
            throw new NotImplementedException();
        }
    }
}
