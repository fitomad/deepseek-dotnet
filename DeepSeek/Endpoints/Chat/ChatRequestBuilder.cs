using Fitomad.DeepSeek.Endpoints;
using Fitomad.DeepSeek.Extensions;
using Fitomad.DeepSeek.Entities.Chat;

namespace Fitomad.DeepSeek.Endpoints.Chat;

public sealed class ChatRequestBuilder
{
    private ChatRequest request = new ChatRequest();

    private const string SystemRole = "system";
    private const string UserRole = "user";
    private const string AssistantRole = "assistant";
    private const string ToolRole = "role";


    public ChatRequestBuilder WithModel(ChatModelType modelKind)
    {
        return WithModel(modelKind.GetValue());
    }

    public ChatRequestBuilder WithModel(string name)
    {
        request.ModelName = name;
        return this;
    }

    public ChatRequestBuilder WithSystemMessage(string content)
    {
        var systemMessage = new ChatRequest.Message();
        systemMessage.Role = SystemRole;
        systemMessage.Content = content;

        request.Messages.Add(systemMessage);

        return this;
    }

    public ChatRequestBuilder WithUserMessage(string content)
    {
        var userMessage = new ChatRequest.Message();
        userMessage.Role = UserRole;
        userMessage.Content = content;

        request.Messages.Add(userMessage);

        return this;
    }

    public ChatRequestBuilder WithAssistantMessage(string content)
    {
        var assistantMessage = new ChatRequest.Message();
        assistantMessage.Role = AssistantRole;
        assistantMessage.Content = content;

        request.Messages.Add(assistantMessage);

        return this;
    }

    public ChatRequestBuilder WithToolMessage(string content, string repondsToolCallId)
    {
        var toolMessage = new ChatRequest.ToolMessage();
        toolMessage.Role = ToolRole;
        toolMessage.Content = content;
        toolMessage.ToolCallId = repondsToolCallId;

        request.Messages.Add(toolMessage);

        return this;
    }

    public ChatRequestBuilder WithFrequencyPenalty(double value)
    {
        request.FrequencyPenalty = value;
        return this;
    }

    public ChatRequestBuilder WithMaximunTokensCount(int count)
    {
        request.MaximumTokens = count;
        return this;
    }

    public ChatRequestBuilder WithCompletionChoicesCount(int count)
    {
        request.CompletionChoices = count;
        return this;
    }

    public ChatRequestBuilder WithPresencePenalty(double value)
    {
        request.PresencePenalty = value;
        return this;
    }

    public ChatRequestBuilder WithReponseFormat(ChatResponseFormat format)
    {
        var responseFormat = new ChatRequest.Response
        {
            Type = format.GetValue()
        };

        request.ResponseFormat = responseFormat;
        return this;
    }

    public ChatRequestBuilder WithTemperatute(Temperature value)
    {
        return WithTemperatute(value.GetValue());
    }

    public ChatRequestBuilder WithTemperatute(double value)
    {
        request.Temperature = value;

        return this;
    }

    public ChatRequestBuilder WithUserId(string userId)
    {
        request.User = userId;
        return this;
    }

    public ChatRequest Build()
    {
        if(request.Temperature.IsOutOfOpenAIRange())
        {
            throw new DeepSeekException($"Temperature parameter is out of range. Current value:({request.Temperature})");
        }

        if(request.FrequencyPenalty.IsOutOfOpenAIRange()) 
        {
            throw new DeepSeekException($"Frequency Penalty parameter is out of range. Current value:({request.FrequencyPenalty})");
        }

        if(request.PresencePenalty.IsOutOfOpenAIRange()) 
        {
            throw new DeepSeekException($"Presence Penalty parameter is out of range. Current value:({request.PresencePenalty})");
        }

        if(string.IsNullOrEmpty(request.ModelName))
        {
            throw new DeepSeekException("A model name is mandatory.");
        }

        if(request.Messages.Count == 0)
        {
            throw new DeepSeekException("You must provide one message at least.");
        } 

        return request;
    }
}