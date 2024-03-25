mergeInto(LibraryManager.library, {

  ShowFullScreenAdv: function () {
    ysdk.adv.showFullscreenAdv({
      callbacks: {
        onClose: function (wasShown) {
          // some action after close
        },
        onError: function (error) {
          // some action on error
        }
      }
    });
  },

  ShowRewardAdv: function () {
    ysdk.adv.showRewardedVideo({
      callbacks: {
        onOpen: () => {
          console.log('Video ad open.');
        },
        onRewarded: () => {
          // console.log('Rewarded!');
          SendMessage("GameManager", "ReceiveReward");
        },
        onClose: () => {
          console.log('Video ad closed.');
        },
        onError: (e) => {
          console.log('Error while open video ad:', e);
        }
      }
    });
  }

});