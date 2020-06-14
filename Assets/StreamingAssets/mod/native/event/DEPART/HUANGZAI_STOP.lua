trigger = function()
    return gm_depart().buffers:is_valid('HUANGZAI') == true
            and gm_depart().buffers:exist_days('HUANGZAI') > 10
end

title = function ()
    return {'HUANGZAI_STOP_TITLE', gm_depart().name}
end
desc = function ()
    return {'HUANGZAI_STOP_DESC', gm_depart().name}
end

occur_days = function ()
    if gm_depart().crop_grow_percent < 20 then
        return 7
    end

    if gm_depart().crop_grow_percent < 30 then
        return 15
    end

    return 50
end

options = 
{
    OPTION_1 = 
    {
        selected = function ()
            gm_depart().buffers:set_invalid('HUANGZAI')
        end,

        tooltip = function ()
            return { {'DEPART_BUFFER_INVAILD', gm_depart().name, 'HUANGZAI'} }
        end
    }
}