using Xunit;

namespace Susalem.EntityFrameworkCore;

[CollectionDefinition(SusalemTestConsts.CollectionDefinitionName)]
public class SusalemEntityFrameworkCoreCollection : ICollectionFixture<SusalemEntityFrameworkCoreFixture>
{

}
