using System.Collections.Generic;
using Gex.Models;
using Gex.Models.Enums;

namespace Gex.Fakers;
public class MateriaFaker
{
    public static IEnumerable<Materia> Get(int limit)
    {
        for (int i = 0; i < new Random().Next(limit); i++)
        {
            yield return new Materia() { Id = Faker.RandomNumber.Next(), Nombre = Faker.Internet.UserName(), Tipo = Faker.Enum.Random<MateriaTipo>() };
        }
    }
}