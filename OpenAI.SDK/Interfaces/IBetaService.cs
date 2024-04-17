﻿namespace OpenAI.Interfaces;

public interface IBetaService
{
    public IAssistantService Assistants { get; }

    public IMessageService Messages { get; }

    public IThreadService Threads { get; }

    public IRunService Runs { get; }
}
