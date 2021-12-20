export interface Result<Type = void> {
    data: Type,
    messages: string[],
    succeeded: boolean
}; 