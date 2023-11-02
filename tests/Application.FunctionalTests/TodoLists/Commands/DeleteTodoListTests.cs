using KyleBurke.Tech.Application.TodoLists.Commands.CreateTodoList;
using KyleBurke.Tech.Application.TodoLists.Commands.DeleteTodoList;
using KyleBurke.Tech.Domain.Entities;

using static KyleBurke.Tech.Application.FunctionalTests.Testing;

namespace KyleBurke.Tech.Application.FunctionalTests.TodoLists.Commands;
public class DeleteTodoListTests : BaseTestFixture
{
    [Test]
    public async Task ShouldRequireValidTodoListId()
    {
        var command = new DeleteTodoListCommand(99);
        await FluentActions.Invoking(() => SendAsync(command)).Should().ThrowAsync<NotFoundException>();
    }

    [Test]
    public async Task ShouldDeleteTodoList()
    {
        var listId = await SendAsync(new CreateTodoListCommand
        {
            Title = "New List"
        });

        await SendAsync(new DeleteTodoListCommand(listId));

        var list = await FindAsync<TodoList>(listId);

        list.Should().BeNull();
    }
}
