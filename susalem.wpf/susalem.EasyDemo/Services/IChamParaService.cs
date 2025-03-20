using susalem.EasyDemo.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace susalem.EasyDemo.Services
{
    public interface IChamParaService
    {
        List<ChemicalParaModel> FindAllParas();
        ChemicalParaModel FindParaById(int id);
        int AddPara(ChemicalParaModel para);
        int EditPara(ChemicalParaModel para);
        int DeletePara(int id);
    }
}
