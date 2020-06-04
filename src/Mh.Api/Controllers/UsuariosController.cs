using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Mh.Api.ViewModels;
using Mh.Business.Interfaces;
using Mh.Business.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Mh.Api.Controllers
{
    [Route("api/usuarios")]
    public class UsuariosController : MainController
    {
        private readonly IUsuarioRepository _usuarioRepository;
        private readonly IUsuarioService _usuarioService;
        private readonly IMapper _mapper;

        public UsuariosController(IUsuarioRepository usuarioRepository, IMapper mapper, IUsuarioService usuarioService, INotificador notificador) : base(notificador)
        {
            _usuarioRepository = usuarioRepository;
            _mapper = mapper;
            _usuarioService = usuarioService;
        }

        [HttpGet]
        [Route("AllUsuarios")]
        public async Task<IEnumerable<UsuarioViewModel>> ObterTodos()
        {
            var usuario = _mapper.Map<IEnumerable<UsuarioViewModel>>(await _usuarioRepository.ObterTodos());
            return usuario;
        }

        [HttpGet("{id:int}")]
        [Route("GetUsuarioId/{id:int}")]
        public async Task<ActionResult<UsuarioViewModel>> ObterPorId(int id)
        {
            var usuario = await ObterUsuario(id);

            if (usuario == null) return NotFound();

            return usuario;
        }

        [HttpPost]
        [Route("InsertUsuario")]
        public async Task<ActionResult<UsuarioViewModel>> Adicionar(UsuarioViewModel usuarioViewModel)
        {

            if (!ModelState.IsValid) return CustomResponse(ModelState);
            await _usuarioService.Adicionar(_mapper.Map<Usuario>(usuarioViewModel));
            return CustomResponse(usuarioViewModel);
        }


        [HttpPut("{id:int}")]
        [Route("UpdateUsuario/{id:int}")] 
        public async Task<ActionResult<UsuarioViewModel>> Atualizar(int id, UsuarioViewModel usuarioViewModel)
        {
            if (id != usuarioViewModel.Id)
            {
                NotificarErro("O ID informado não está correto.");
                return CustomResponse(usuarioViewModel);
            }

            if (!ModelState.IsValid) return CustomResponse(ModelState);

            await _usuarioService.Atualizar(_mapper.Map<Usuario>(usuarioViewModel));


            return CustomResponse(usuarioViewModel);
        }


        [HttpDelete("{id:int}")]
        [Route("DeleteUsuario/{id:int}")]
        public async Task<ActionResult<UsuarioViewModel>> Excluir(int id)
        {
            var usuarioViewModel = await ObterUsuario(id);
            if (usuarioViewModel == null) return NotFound();

            await _usuarioService.Remover(id);
         
            return CustomResponse(usuarioViewModel);
        }

        public async Task<UsuarioViewModel> ObterUsuario(int id)
        {
            return _mapper.Map<UsuarioViewModel>(await _usuarioRepository.ObterUsuario(id));
        }
    }


}