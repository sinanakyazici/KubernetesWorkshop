1 - build icin  
	- oldugu dizini vermek icin .
	- bir ust dizine cikmak icin ../
	- iki ust dizine cikmak icin ../../
	build ederken dockerfile icindeki dosyalara erisebilmesi icin bu yolun dogru verilmesi gerekiyor. icinde path neredeyse ona gore vermek lazim
	-f komutu dockerfile in path ini vermek lazim
	-t image adini belirtir tag lemek icin
	docker build -t kubeservice.cmd -f Dockerfile ../../
	
2 - docker run


Seçenek	Açıklama
-d	Arka planda çalıştırır.
-it	Etkileşimli terminal başlatır. (kullanici girdisi ve termimal ile kontainer izleme)
-p	Port yönlendirmesi yapar (örn: -p 8080:80). [disaridan kontainer erismek icin gereken port]:[kontainer in icindeki port]
-e	Ortam değişkeni tanımlar.
-v veya --mount	Klasör paylaşımı yapar.
--rm	Konteyner kapandığında otomatik olarak silinir.
--name	Konteynere özel bir isim verir.

docker run -dit --name Kube.Service.Cmd -p 5000:80 kubeservice.cmd