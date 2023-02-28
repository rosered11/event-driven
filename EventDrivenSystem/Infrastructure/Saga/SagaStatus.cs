namespace Rosered11.Infrastructure.Saga;
public enum SagaStatus {
        STARTED, FAILED, SUCCEEDED, PROCESSING, COMPENSATING, COMPENSATED
}