public interface IInAppReviewService
{
    void Init(ICoroutineRunner coroutineRunner);
    void RequestAppReview();
}