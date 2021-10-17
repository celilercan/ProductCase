using ProductCase.Common.Enums;

namespace ProductCase.Dto
{
    public class ApiResultDto<T>
    {
        public ApiResultDto()
        {
            this.Status = ResponseStatusEnum.Success;
        }

        public T Data { get; set; }
        public string Message { get; set; }
        public ResponseStatusEnum Status { get; set; }
    }
}
