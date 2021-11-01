package com.SEP3.CarRentalAPI.Controllers;

import com.SEP3.CarRentalAPI.Car;
import org.springframework.web.bind.annotation.GetMapping;
import org.springframework.web.bind.annotation.RequestParam;
import org.springframework.web.bind.annotation.RestController;

import java.util.concurrent.atomic.AtomicLong;

@RestController
public class CarController {
    private static final String templateName = "name=" + "%s";
    private static final String templateModel = "model=" + "%s";
    private final AtomicLong counter = new AtomicLong();

    @GetMapping("/getDemo")
    public Car carDemo(@RequestParam(value = "name", defaultValue = " ") String name, @RequestParam(value = "model", defaultValue = " ") String model){
        return new Car(counter.incrementAndGet(), String.format(templateName, name), String.format(templateModel, model));
    }
}

