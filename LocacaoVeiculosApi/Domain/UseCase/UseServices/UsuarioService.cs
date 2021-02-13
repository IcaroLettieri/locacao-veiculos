using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using LocacaoVeiculosApi.Domain.Authentication;
using LocacaoVeiculosApi.Domain.Entities;
using LocacaoVeiculosApi.Domain.ViewModel;
using LocacaoVeiculosApi.Infra.Database;

namespace LocacaoVeiculosApi.Domain.UseCase.UseServices
{
    public class UsuarioService
    {
        public UsuarioService(IUsuarioRepository repository){
          this.repository = repository;
        }

        public UsuarioService(UsuarioRepository userRepository)
        {
        }

        private IUsuarioRepository repository;

        public async Task<UsuarioJwt> Login(Usuario user, IToken token){
           var loggedUser = await repository.FindByLoginAndPassword(user.CpfMatricula, user.Senha);
           if(loggedUser == null) throw new UsuarioNotFound("Usuário e senha inválidos.");
           return new UsuarioJwt(){
             id = loggedUser.Id,
             login = loggedUser.CpfMatricula,
             userType = loggedUser.TipoUsuario.ToString(),
             Token = token.GerarToken(loggedUser)
           };
        }

        public async Task<ICollection<UsuarioView>> All()
        {
            throw new NotImplementedException();
        }

        public async Task Save(Usuario user) {
          //if(user.TipoUsuario == null) user.TipoUsuario = UserRole.Operador;
          
          if(user.Id > 0){
            var size = await repository.CountByIdAndUser(user.Id, user.CpfMatricula);
            if(size > 0) throw new UsuarioUnico("CPF ou Matrícula já cadastrado.");
            await repository.Update(user);
          }
          else{
            var size = await repository.CountByUser(user.CpfMatricula);
            if(size > 0) throw new UsuarioUnico("CPF ou Matrícula já cadastrado.");
            await repository.Save(user);
          }
        }
    }
}