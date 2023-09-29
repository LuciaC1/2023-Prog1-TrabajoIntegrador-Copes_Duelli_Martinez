using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmpresaEnvÍoDto.ClasesDto
{
    public class ValidacionDto
    {
        public bool Resultado { get; set; }
        public List<Error> Errores { get; set; }
    }
}
