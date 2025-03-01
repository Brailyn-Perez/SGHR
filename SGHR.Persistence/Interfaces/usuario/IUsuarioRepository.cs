﻿
using SGHR.Domain.Base;
using SGHR.Domain.Entities.usuario;
using SGHR.Domain.Repository;

namespace SGHR.Persistence.Interfaces.usuario
{
    public interface IUsuarioRepository : IBaseRepository<Usuario>
    {
        public Task<OperationResult> ValidarUsuario(Usuario usuario);
    }
}
