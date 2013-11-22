using AutorizacionRuleEngine.Tests.Dummies;
using NUnit.Framework;
using Sigs.Expertos;
using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace AutorizacionRuleEngine.Tests
{
    [TestFixture]
    public class MotorInferenciaTest
    {
        Dumms dumsFactory = new Dumms();
        /*
         * limite de autorizaciones General en tiempo. * may be Useless
         * limite de autorizaciones General por Cobertura en tiempo.
         * limite de autorizaciones General por Prestacion en tiempo.
         * limite de autorizaciones General por Subgrupo en tiempo.
         * limite de autorizaciones General por TipoAutorizacion (Grupo) en tiempo.
         * limite de autorizaciones de un afiliado especifico en tiempo.
         * limite de autorizaciones de un afiliado especifico por Cobertura En Tiempo.
         * limite de autorizaciones de un afiliado especifico por Prestacion En Tiempo.
         * limite de autorizaciones de un afiliado especifico por SubGrupo en Tiempo.
         * limite de autorizaciones de un afiliado especifico por Grupo en Tiempo.
         * limite de autorizaciones de un afiliado especifico por Prestadora en Tiempo.
         
         * limite de autorizaciones de un Prestadora especifico en tiempo.
         * limite de autorizaciones de un Prestadora especifico por Cobertura En Tiempo.
         * limite de autorizaciones de un Prestadora especifico por Prestacion En Tiempo.
         * limite de autorizaciones de un Prestadora especifico por SubGrupo en Tiempo.
         * limite de autorizaciones de un Prestadora especifico por Grupo en Tiempo.
         * limite de autorizaciones de un Prestadora especifico por Afiliado en Tiempo.
         */

        [Test]
        public void No_Aprueba_Afiliados_Sin_Cotizaciones()
        {
            MotorInferencia engine = new MotorInferencia();

            var auth = dumsFactory.AutorizacionConConsultaEspecializadaPYP;

            engine.Procesar(auth);

            var expected = 0;

            Assert.AreEqual(expected, auth.Prestaciones.Count);
        }

        [Test]
        public void Aprueba_100_Porciento_Servicios_Ambulatorios()
        {
            MotorInferencia engine = new MotorInferencia();

            var auth = dumsFactory.AutorizacionConConsultaEspecializadaPYP;
            auth.Afiliado.CotizacionesConsecutivasPDSS = 12;

            engine.Procesar(auth);

            var expected = 500;
            var actual = auth.MontoAprobado;

            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void Aprueba_70_Porciento_Servicios_Odontologicos()
        {
            var auth = dumsFactory.AutorizacionConServiciosOdontologicos;
            auth.Afiliado.CotizacionesConsecutivasPDSS = 12;

            MotorInferencia engine = new MotorInferencia();

            engine.Procesar(auth);

            var expected = 700;
            var actual = auth.MontoAprobado;

            Assert.AreEqual(expected, actual);
        }
    }
}
