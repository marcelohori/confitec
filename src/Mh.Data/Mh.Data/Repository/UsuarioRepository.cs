using Mh.Business.Interfaces;
using Mh.Business.Models;
using Mh.Data.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Mh.Data.Repository
{
    public class UsuarioRepository : Repository<Usuario>,IUsuarioRepository
    {
        public UsuarioRepository(MeuDbContext context) : base (context)
        {

        }

        public async Task<Usuario> ObterUsuario(int id)
        {
            return await Db.Usuarios.AsNoTracking()                
                .FirstOrDefaultAsync(c => c.Id == id);
        }

    }
}
