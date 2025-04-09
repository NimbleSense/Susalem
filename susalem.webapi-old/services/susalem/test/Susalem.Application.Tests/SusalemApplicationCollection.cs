using Susalem.EntityFrameworkCore;
using Xunit;

namespace Susalem;

[CollectionDefinition(SusalemTestConsts.CollectionDefinitionName)]
public class SusalemApplicationCollection : SusalemEntityFrameworkCoreCollectionFixtureBase
{

}
