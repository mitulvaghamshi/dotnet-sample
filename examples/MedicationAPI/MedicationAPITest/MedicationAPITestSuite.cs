using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace MedicationAPITest;

public class MedicationAPITestSuite
{
    [Fact]
    public async Task Get_NoInput_ReturnsAllMedications()
    {
        var controller = new MedicationsController(await GetContext("Get_NoInput"));

        var actionResult = await controller.GetMedications();
        Assert.IsType<ActionResult<IEnumerable<Medication>>>(actionResult);
        Assert.IsType<List<Medication>>(actionResult.Value);

        var medications = (List<Medication>)actionResult.Value;
        Assert.Equal(3, medications.Count);
        Assert.Equal(1, medications[0].MedicationId);
        Assert.Equal(1.25M, medications[1].MedicationCost);
        Assert.Equal("Advil", medications[2].MedicationDescription);
    }

    [Fact]
    public async Task Get_Input_ReturnsMedication()
    {
        var controller = new MedicationsController(await GetContext("Get_Input"));

        var actionResult = await controller.GetMedication(2);
        Assert.IsType<ActionResult<Medication>>(actionResult);
        Assert.IsType<Medication>(actionResult.Value);

        var medication = actionResult.Value;
        Assert.Equal(2, medication.MedicationId);
        Assert.Equal(1.25M, medication.MedicationCost);
        Assert.Equal("Ibprofen", medication.MedicationDescription);
    }

    [Fact]
    public async Task Put_Input_ReturnsNoContent()
    {
        var controller = new MedicationsController(await GetContext("Put_Input"));

        var medication = new Medication { MedicationId = 3, MedicationCost = 2.25M, MedicationDescription = "Tylenol" };

        var actionResult = await controller.PutMedication(3, medication);

        Assert.IsType<NoContentResult>(actionResult);

        var result = (NoContentResult?)actionResult;

        Assert.NotNull(result);
        Assert.Equal(204, result.StatusCode);
    }

    [Fact]
    public async Task Put_InvalidInput_ReturnsBadRequest()
    {
        var controller = new MedicationsController(await GetContext("Put_InvalidInput"));

        var actionResult = await controller.PutMedication(101, new Medication { MedicationId = 3, MedicationCost = 2.25M, MedicationDescription = "Tylenol" });

        Assert.IsType<BadRequestResult>(actionResult);

        var result = (BadRequestResult?)actionResult;
        Assert.NotNull(result);
        Assert.Equal(400, result.StatusCode);
    }

    /// <summary>
    /// System.InvalidOperationException : 
    ///     The instance of entity type 'Medication' cannot be tracked because another instance 
    ///     with the same key value for {'MedicationId'} is already being tracked. 
    ///     When attaching existing entities, ensure that only one entity instance with a given key value is attached. 
    ///     Consider using 'DbContextOptionsBuilder.EnableSensitiveDataLogging' to see the conflicting key values.
    /// </summary>
    /// <returns></returns>
    [Fact]
    public async Task Post_Input_ReturnsCreatedAtAction()
    {
        var controller = new MedicationsController(await GetContext("Post_Input"));

        var actionResult = await controller.PostMedication(new Medication { MedicationId = 4, MedicationCost = 5.0M, MedicationDescription = "Advil Plus" });

        Assert.IsType<CreatedAtActionResult>(actionResult.Result);

        var result = (CreatedAtActionResult?)actionResult.Result;
        Assert.NotNull(result);
        Assert.Equal(201, result.StatusCode);
        Assert.Equal("GetMedication", result.ActionName);
        Assert.NotNull(result.RouteValues);
        Assert.IsType<Medication>(result.Value);

        var routeValues = result.RouteValues;
        Assert.Equal(4, routeValues.Values.ElementAt(0));

        var medication = (Medication)result.Value;
        Assert.NotNull(medication);
        Assert.Equal(4, medication.MedicationId);
        Assert.Equal(5.0M, medication.MedicationCost);
        Assert.Equal("Advil Plus", medication.MedicationDescription);
    }

    /// <summary>
    /// System.InvalidOperationException : 
    ///     The instance of entity type 'Medication' cannot be tracked because another instance 
    ///     with the same key value for {'MedicationId'} is already being tracked. 
    ///     When attaching existing entities, ensure that only one entity instance with a given key value is attached. 
    ///     Consider using 'DbContextOptionsBuilder.EnableSensitiveDataLogging' to see the conflicting key values.
    /// </summary>
    /// <returns></returns>
    [Fact]
    public async Task Post_ExistingData_ReturnsConflict()
    {
        var controller = new MedicationsController(await GetContext("Post_ExistingData"));

        var actionResult = await controller.PostMedication(new Medication { MedicationId = 3, MedicationCost = 1.5M, MedicationDescription = "Advil" });
        Assert.IsType<ConflictResult>(actionResult.Result);

        var result = (ConflictResult?)actionResult.Result;
        Assert.NotNull(result);
        Assert.Equal(409, result.StatusCode);
    }

    [Fact]
    public async Task Delete_InvalidInput_ReturnsNotFound()
    {
        var controller = new MedicationsController(await GetContext("Delete_InvalidInput"));

        var actionResult = await controller.DeleteMedication(101);

        Assert.IsType<NotFoundResult>(actionResult);

        var result = (NotFoundResult?)actionResult;
        Assert.NotNull(result);
        Assert.Equal(404, result.StatusCode);
    }

    [Fact]
    public async Task Delete_Input_ReturnsNoContent()
    {
        var controller = new MedicationsController(await GetContext("Delete_Input"));

        var actionResult = await controller.DeleteMedication(3);

        Assert.IsType<NoContentResult>(actionResult);

        var result = (NoContentResult?)actionResult;
        Assert.NotNull(result);
        Assert.Equal(204, result.StatusCode);
    }

    private static async Task<CHDBContext> GetContext(string name)
    {
        var options = new DbContextOptionsBuilder<CHDBContext>().UseInMemoryDatabase(databaseName: name).Options;

        var _context = new CHDBContext(options);

        _context.Medications.AddRange(
           new Medication { MedicationId = 1, MedicationCost = 1.0M, MedicationDescription = "Pain Reliever" },
           new Medication { MedicationId = 2, MedicationCost = 1.25M, MedicationDescription = "Ibprofen" },
           new Medication { MedicationId = 3, MedicationCost = 1.5M, MedicationDescription = "Advil" }
        );

        await _context.SaveChangesAsync();

        return _context;
    }
}
