﻿using DevIO.Business.Interfaces;
using DevIO.Business.Models;
using DevIO.Business.Models.Validations.Documentos;

namespace DevIO.Business.Services;

public class ProdutoService : BaseService, IProdutoService
{
    public async Task Adicionar(Produto produto)
    {
        if (!ExecutarValidacao(new ProdutoValidation(), produto)) return;
    }

    public async Task Atualizar(Produto produto)
    {
        if (!ExecutarValidacao(new ProdutoValidation(), produto)) return;
    }

    public async Task Remover(Guid id)
    {
        throw new NotImplementedException();
    }
}