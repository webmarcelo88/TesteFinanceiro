using FizzWare.NBuilder;
using Financeiro.API.Controllers;
using Financeiro.Dominio;
using Financeiro.Services.Interface;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Linq;
using System.Net;

namespace Financeiro.Api.Test
{
    [TestClass]
    public class BalancoTest
    {
        private BalancoController _controller;
        private Mock<IBalancoServices> _mock;

        [TestInitialize]
        public void Inicializar()
        {
            _mock = new Mock<IBalancoServices>();

            _controller = new BalancoController(_mock.Object);
        }

        [TestMethod]
        public void get_balanco_sucesso()
        {
            var listaRetorno = Builder<BalancoMensal>.CreateListOfSize(10).Build().ToList();

            _mock.Setup(_ => _.BuscarBalancoMensal(It.IsAny<DateTime?>(), It.IsAny<DateTime?>())).Returns(listaRetorno);
            var retorno = _controller.GetBalancoMensal(null, null);

            Assert.IsTrue(((ObjectResult)retorno).StatusCode == (int)HttpStatusCode.OK);
        }

        [TestMethod]
        public void get_balanco_erro()
        {
            _mock.Setup(_ => _.BuscarBalancoMensal(It.IsAny<DateTime?>(), It.IsAny<DateTime?>())).Throws(new Exception("Ocorreu um erro ao gerar balanço"));

            var retorno = _controller.GetBalancoMensal(null, null);

            Assert.IsTrue(((ContentResult)retorno).StatusCode == (int)HttpStatusCode.InternalServerError);
        }
    }
}
