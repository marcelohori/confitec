using Mh.Business.Interfaces;
using Mh.Business.Models;
using Mh.Business.Models.Validation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mh.Business.Services
{
    public class UsuarioService : BaseService, IUsuarioService
    {
        private readonly IUsuarioRepository _usuarioRepository;

        public UsuarioService(IUsuarioRepository usuarioRepository,                               
                                INotificador notificador) : base(notificador)
        {
            _usuarioRepository = usuarioRepository;           
        }


        public async Task<bool> Adicionar(Usuario usuario)
        {
            if (!ExecutarValidacao(new UsuarioValidation(), usuario)) return false;

            if (_usuarioRepository.Buscar(f => f.Email == usuario.Email).Result.Any())
            {
                Notificar("Já existe um usuário com este e-mail informado.");
                return false;
            }

            await _usuarioRepository.Adicionar(usuario);
            return true;
        }

        public async Task<bool> Atualizar(Usuario usuario)
        {
            if (!ExecutarValidacao(new UsuarioValidation(), usuario)) return false;

            if (_usuarioRepository.Buscar(f => f.Email == usuario.Email && f.Id != usuario.Id).Result.Any())
            {
                Notificar("Já existe um usuário com este e-mail infomado.");
                return false;
            }

            await _usuarioRepository.Atualizar(usuario);
            return true;
        }

        public async Task<bool> Remover(int id)
        {           
            await _usuarioRepository.Remover(id);
            return true;
        }

        public void Dispose()
        {
            _usuarioRepository?.Dispose();          
        }
    }
}
