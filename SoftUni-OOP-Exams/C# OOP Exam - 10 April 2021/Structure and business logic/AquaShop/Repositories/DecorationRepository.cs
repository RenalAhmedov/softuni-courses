using AquaShop.Models.Decorations.Contracts;
using AquaShop.Repositories.Contracts;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace AquaShop.Repositories
{
    public class DecorationRepository : IRepository<IDecoration>
    {
        private List<IDecoration> decorations;
        public DecorationRepository()
        {
            decorations = new List<IDecoration>();
        }
        public IReadOnlyCollection<IDecoration> Models => this.decorations.AsReadOnly();

        public void Add(IDecoration model)
        {
            decorations.Add(model);
        }

        public IDecoration FindByType(string type) => this.decorations.FirstOrDefault(x => x.GetType().Name == type);
 
        public bool Remove(IDecoration model)
        {
            if (decorations.Contains(model))
            {
                return false;
            }
            else
            {
                decorations.Remove(model);
                return true;
            }
        }
    }
}
