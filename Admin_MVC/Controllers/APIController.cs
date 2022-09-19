using Microsoft.AspNetCore.Mvc;
using Admin_MVC.Data;
using Admin_MVC.Models;
using Admin_MVC.Business.Dtos;
using AutoMapper;
using Admin_MVC.Business.Enums;

namespace Admin_MVC.Controllers
{

    public class APIController : Controller
    {

        private readonly DataContext _context;
        private readonly IMapper _mapper;

        public APIController(DataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult GetProdutos()
        {
            var produtos = new List<ProdutoData>();
            var produtosDto = new List<ProdutoApiDto>();
            try
            {
                produtos = _context.Produtos.ToList();
                foreach (var produto in produtos)
                {
                    produtosDto.Add( new ProdutoApiDto
                    {
                        Id = produto.Id,
                        Usuario = _context.Usuarios.Find(produto.IdUsuario).Nome,
                        Nome = produto.Nome,
                        idCategoria = produto.idCategoria,
                        Valor = produto.Valor,
                        linkShoppe = produto.linkShoppe,
                        PathFotoPrimaria = produto.PathFotoPrimaria,
                        PathFotoSecundaria = produto.PathFotoSecundaria
                    });
                }
                return Ok(produtosDto);

            } catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        public IActionResult GetProdutoById(int id)
        {
            ProdutoData produto = new ProdutoData();
            try
            {
                produto = _context.Produtos.Find(id);
                if (produto != null)
                {
                    var produtoDto = new ProdutoApiDto(){
                        Id = produto.Id,
                        Usuario = _context.Usuarios.Find(produto.IdUsuario).Nome,
                        Nome = produto.Nome,
                        idCategoria = produto.idCategoria,
                        Valor = produto.Valor,
                        linkShoppe = produto.linkShoppe,
                        PathFotoPrimaria = produto.PathFotoPrimaria,
                        PathFotoSecundaria = produto.PathFotoSecundaria
                    };
                    return Ok(produtoDto);
                }
            }catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

            return NotFound();
        }

        [HttpGet]
        public IActionResult GetProdutosByUser(int id)
        {
            var produtos = new List<ProdutoData>();
            var produtosDto = new List<ProdutoApiDto>();
            try
            {
                produtos = _context.Produtos.Where(x => x.IdUsuario == id).ToList();
                foreach (var produto in produtos)
                {
                    produtosDto.Add(new ProdutoApiDto
                    {
                        Id = produto.Id,
                        Usuario = _context.Usuarios.Find(produto.IdUsuario).Nome,
                        Nome = produto.Nome,
                        idCategoria = produto.idCategoria,
                        Valor = produto.Valor,
                        linkShoppe = produto.linkShoppe,
                        PathFotoPrimaria = produto.PathFotoPrimaria,
                        PathFotoSecundaria = produto.PathFotoSecundaria
                    });
                }
                return Ok(produtosDto);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

        [HttpGet]
        public IActionResult GetProdutosByCategoria(int id)
        {
            var produtos = new List<ProdutoData>();
            var produtosDto = new List<ProdutoApiDto>();
            try
            {
                produtos = _context.Produtos.Where(x => x.idCategoria == id).ToList();
                foreach (var produto in produtos)
                {
                    produtosDto.Add(new ProdutoApiDto
                    {
                        Id = produto.Id,
                        Usuario = _context.Usuarios.Find(produto.IdUsuario).Nome,
                        Nome = produto.Nome,
                        idCategoria = produto.idCategoria,
                        Valor = produto.Valor,
                        linkShoppe = produto.linkShoppe,
                        PathFotoPrimaria = produto.PathFotoPrimaria,
                        PathFotoSecundaria = produto.PathFotoSecundaria
                    });
                }
                return Ok(produtosDto);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

        [HttpGet]
        public IActionResult GetUsuarios()
        {
            var usuarios = new List<UsuarioData>();
            var usuariosDto = new List<UsuarioApiDto>();
            try
            {
                usuarios = _context.Usuarios.Where(x => x.NivelAcesso == (int)NivelAcessoEnum.Artista).ToList();
                foreach(var usario in usuarios)
                {
                    usuariosDto.Add( _mapper.Map<UsuarioApiDto>(usario));
                }
                return Ok(usuariosDto);
            }catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        public IActionResult GetUsuarioById(int id)
        {
            var usuario = new UsuarioData();
            try
            {
                usuario = _context.Usuarios.Find(id);
                if(usuario != null)
                    return Ok( _mapper.Map<UsuarioApiDto>(usuario));
                return NotFound();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        public IActionResult GetCategorias()
        {
            var produtos = new List<CategoriaData>();
            try
            {
                produtos = _context.Categorias.ToList();
                return Ok(produtos);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

        [HttpGet]
        public IActionResult GetCategoriaById(int id)
        {
            var produto = new CategoriaData();
            try
            {
                produto = _context.Categorias.Find(id);
                if(produto != null)
                    return Ok(produto);
                return NotFound();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

        [HttpGet]
        public IActionResult GetSobre()
        {
            var termo = _context.Termos.Find(1).Sobre;
            return Ok(termo);
        }

        [HttpGet]
        public IActionResult GetTermosUso()
        {
            var termo = _context.Termos.Find(1).TermosDeUso;
            return Ok(termo);
        }

        [HttpGet]
        public IActionResult GetPrivacidade()
        {
            var termo = _context.Termos.Find(1).TermosDePrivacidade;
            return Ok(termo);
        }

    }
}
