using System.Collections.Generic;
using InfraContracts;

namespace CelebContracts
{
    public class GetAllCelebsResponseOk : ResponseOk
    {
        public CelebDto[] Celebs { get; set; }
    }
}