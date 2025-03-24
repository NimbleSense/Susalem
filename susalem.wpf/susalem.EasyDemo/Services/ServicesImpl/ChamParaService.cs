using susalem.EasyDemo.Entities;
using susalem.EasyDemo.Repository;
using susalem.EasyDemo.Services;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace susalem.EasyDemo.Services.ServicesImpl
{
    public class ChamParaService : IChamParaService
    {
        public int AddPara(ChemicalParaModel para)
        {
            int nRet = 0;
            using (JccRepository hc = new JccRepository())
            {
                try
                {
                    hc.ChemicalParas?.Add(para);
                    hc.SaveChanges();
                }
                catch (Exception ex)
                {
                }
            };
            return nRet;
        }

        public int DeletePara(int id)
        {
            int nRet = 0;
            using (JccRepository hc = new JccRepository())
            {
                try
                {
                    hc.ChemicalParas?.Remove(hc.ChemicalParas.Where(u => u.Id == id).FirstOrDefault()!);
                    hc.SaveChanges();
                }
                catch (Exception ex)
                {
                }
            };
            return nRet;
        }

        public int EditPara(ChemicalParaModel para)
        {
            int nRet = 0;
            using (JccRepository hc = new JccRepository())
            {
                try
                {
                    hc.ChemicalParas?.Update(para);
                    hc.SaveChanges();
                }
                catch (Exception ex)
                {
                }
            };
            return nRet;
        }

        public List<ChemicalParaModel> FindAllParas()
        {
            List<ChemicalParaModel> result = new List<ChemicalParaModel>();
            using (JccRepository hc = new JccRepository())
            {
                try
                {
                    result = hc.ChemicalParas?.Select(r => r).ToList();
                }
                catch (Exception ex)
                {
                }
            };
            return result;
        }

        public ChemicalParaModel FindParaById(int id)
        {
            ChemicalParaModel result = new ChemicalParaModel();
            using (JccRepository hc = new JccRepository())
            {
                try
                {
                    result = hc.ChemicalParas?.Where(r => r.Id == id).FirstOrDefault();
                }
                catch (Exception ex)
                {
                }
            };
            return result;
        }
    }
}
