using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sigs.AutorizacionesOnline.Models.Entities
{
    public enum TipoCobertura
    {
        Desconocida = 0,
        Consultas = 1,
        Laboratorio = 2,
        Anatomía_patológica = 3,
        Radiología_convencional = 4,
        Estudios_radiológicos = 5,
        Mamografías = 6,
        T_A_C = 7,
        R_M = 8,
        Densitometría_ósea = 9,
        Ecografías = 10,
        Neumología = 11,
        Pruebas_cardiológicas = 12,
        Pruebas_neurológicas = 13,
        Endoscopias = 14,
        Material_Sanitario = 15,
        Fármacos = 16,
        Vacunas = 17,
        Sistemas_de_movilización_inmovilización_ortopédicos_ortesis = 18,
        Protesis = 19,
        Rehabilitación = 20,
        Otras_técnicas_de_tratamiento = 21,
        Dialisis = 22,
        Hotelería = 23,
        Uso_de_aparataje = 24,
        Actos_de_enfermería = 25,
        Medicina_Nuclear = 26,
        Otros_honorarios_médicos = 27,
        Hemoterapia = 28,
        Odontología = 29,
        Actos_Quirúrgicos_anestésicos = 30,
        Capitación = 32
    }

    public enum SexoPrestaciones
    {
        Ambos = 0,
        Femenino = 1,
        Masculino = 2,
    }

    public enum SexoAfiliado
    {
        Femenino = 'F',
        Masculino = 'M'
    }
}
