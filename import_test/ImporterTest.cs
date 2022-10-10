using import_saas.Importers;
using import_saas.Models.Db.MySql;
using import_saas.Services;
using Moq;

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

        dbServiceMock.Verify(db => db.GetProduct(It.IsAny<string>()), Times.Exactly(3));
        dbServiceMock.Verify(db => db.CreateProduct(It.IsAny<Product>()), Times.Exactly(3));
    }

    [Fact]
    public void ImportSoftwareAdvice()
    {
        var mapper = MapperTest.InitMapper();

        var filePath = "/feed-products/softwareadvice.json";
        var fileServiceMock = new Mock<IFileService>();
        fileServiceMock.Setup(fs => fs.SoftwareAdvice)
            .Returns(AppContext.BaseDirectory + filePath);

        var dbServiceMock = new Mock<IDbService>();

        var importer = new SoftwareAdviceImporter(mapper, dbServiceMock.Object, fileServiceMock.Object);

        importer.Execute();

        dbServiceMock.Verify(db => db.GetProduct(It.IsAny<string>()), Times.Exactly(2));
        dbServiceMock.Verify(db => db.CreateProduct(It.IsAny<Product>()), Times.Exactly(2));
    }

    [Fact]
    public void CapterraFileNotFoundException()
    {
        var mapper = MapperTest.InitMapper();

        var filePath = "/feed-products/not_exists.yaml";
        var fileServiceMock = new Mock<IFileService>();
        fileServiceMock.Setup(fs => fs.Capterra)
            .Returns(AppContext.BaseDirectory + filePath);

        var dbServiceMock = new Mock<IDbService>();

        var importer = new CapterraImporter(mapper, dbServiceMock.Object, fileServiceMock.Object);

        Action action = () => importer.Execute();

        Assert.Throws<FileNotFoundException>(action);
    }

    [Fact]
    public void SoftwareAdviceInvalid()
    {
        var mapper = MapperTest.InitMapper();

        var filePath = "/feed-products/softwareadvice_invalid.json";
        var fileServiceMock = new Mock<IFileService>();
        fileServiceMock.Setup(fs => fs.SoftwareAdvice)
            .Returns(AppContext.BaseDirectory + filePath);

        var dbServiceMock = new Mock<IDbService>();

        var importer = new SoftwareAdviceImporter(mapper, dbServiceMock.Object, fileServiceMock.Object);

        Action action = () => importer.Execute();

        Exception e = Assert.Throws<Exception>(action);

        Assert.True(e.Message.Equals("Invalid format in following product indeces: 1, 2"));
    }
}