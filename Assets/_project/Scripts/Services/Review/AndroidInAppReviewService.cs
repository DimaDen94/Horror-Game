using System.Collections;
using Google.Play.Review;

public class AndroidInAppReviewService : IInAppReviewService
{
    private ReviewManager _reviewManager;
    private ICoroutineRunner _coroutineRunner;

    private PlayReviewInfo _playReviewInfo;

    public void RequestAppReview() {
        _coroutineRunner.StartCoroutine(RequestReviewFlow());
    }

    public void Init(ICoroutineRunner coroutineRunner)
    {
        _coroutineRunner = coroutineRunner;
        _reviewManager = new ReviewManager();
    }

    private IEnumerator RequestReviewFlow() {
        var requestFlowOperation = _reviewManager.RequestReviewFlow();
        yield return requestFlowOperation;
        if (requestFlowOperation.Error != ReviewErrorCode.NoError)
        {
            // Log error. For example, using requestFlowOperation.Error.ToString().
            yield break;
        }
        _playReviewInfo = requestFlowOperation.GetResult();


        var launchFlowOperation = _reviewManager.LaunchReviewFlow(_playReviewInfo);
        yield return launchFlowOperation;
        _playReviewInfo = null; // Reset the object
        if (launchFlowOperation.Error != ReviewErrorCode.NoError)
        {
            // Log error. For example, using requestFlowOperation.Error.ToString().
            yield break;
        }
    }


}