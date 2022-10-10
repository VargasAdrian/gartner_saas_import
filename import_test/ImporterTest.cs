using import_saas.Importers;
using import_saas.Models.Db.MySql;
using import_saas.Models.Dto;
using import_saas.Services;
using Moq;
using Newtonsoft.Json;

namespace import_test;

public class ImporterTest
{
    [Fact]
    public void ImportCapterra()
    {
        var mapper = MapperTest.InitMapper();

        var filePath = "/feed-products/capterra.yaml";
        var fileServiceMock = new Mock<IFileService>();
        fileServiceMock.Setup(fs => fs.Capterra)
            .Returns(AppContext.BaseDirectory + filePath);

        var dbServiceMock = new Mock<IDbService>();

        var importer = new CapterraImporter(mapper, dbServiceMock.Object, fileServiceMock.Object);

        importer.Execute();

        dbServiceMock.Verify(db => db.CreateProduct(It.IsAny<Product>()), Times.Exactly(3));
    }
}