
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
        private readonly IConfiguration _configuraticon;

        public ClienteService(IClienteRepository clienteRepository,
            ILogger<ClienteService> logger,
            IConfiguration configuraticon)
        {
            _clienteRepository = clienteRepository;
            _logger = logger;
            _configuraticon = configuraticon;
        }
        public async Task<OperationResult> GeAll()
        {
            var result = new OperationResult();
            try
            {
                var cliente = await _clienteRepository.GetAllAsync();
                result.Success = true;
                result.Data = cliente.Select(x => new ClienteDToBase
                {
                    IdCliente = x.IdCliente,
                    NombreCompleto = x.NombreCompleto,
                    Documento = x.Documento,
                    TipoDocumento =x.TipoDocumento,
                    Correo = x.Correo,
                    Estado = x.Estado
                    
                });
            }
            catch (Exception ex) 
            {
                result.Success = false;
                result.Message = _configuraticon["ErrorGetAllCliente"];
                _logger.LogError(ex, result.Message);
            }
            return result;
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
