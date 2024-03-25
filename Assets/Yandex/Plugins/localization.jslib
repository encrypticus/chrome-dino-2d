mergeInto(LibraryManager.library, {

  GetYandexLang: async function () {
    try {
      const ysdk = await YaGames.init();
      let lang = ysdk.environment.i18n.lang;
      let bufferSize = lengthBytesUTF8(lang) + 1;
      let buffer = _malloc(bufferSize);
      stringToUTF8(lang, buffer, bufferSize);
      return buffer;
    } catch (error) {
      console.log("ERROR GetYandexLang", error);
      return navigator.language;
    }
  },
  GetYandexLangAsync: function () {
    YaGames
      .init()
      .then((ysdk) => {
        const lang = ysdk.environment.i18n.lang;
        SendMessage("SettingsManager", "GetYandexLangAsync", lang);
      })
      .catch(() => {
        SendMessage("SettingsManager", "GetYandexLangAsync", navigator.language);
      });
  },
  
});